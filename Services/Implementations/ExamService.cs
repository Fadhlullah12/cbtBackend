using System.Collections;
using cbtBackend.Dtos.RequestModels;
using cbtBackend.Dtos.ResponseModels;
using cbtBackend.Model;
using cbtBackend.Model.Entities;
using cbtBackend.Repositories.Interfaces;
using cbtBackend.Services.Interfaces;

namespace cbtBackend.Services.Implementations
{
    public class ExamService : BaseResponse<Exam>, IExamService
    {
        IQuestionRepository _questionRepository;
        IResultRepository _resultRepository;
        ISubAdminRepository _subAdminRepository;
        IStudentExamRepository _studentExamRepository;
        IStudentSubjectRepository _studentSubjectRepository;
        ISubjectRepository _subjectRepository;
        IExamRepository _examRepository;
        IGetCurrentUser _getCurrentUser;
        IStudentRepository _studentRepository;
        public ExamService(IGetCurrentUser getCurrentUser, ISubAdminRepository subAdminRepository, ISubjectRepository subjectRepository, IExamRepository examRepository, IStudentExamRepository studentExamRepository, IResultRepository resultRepository, IQuestionRepository questionRepository, IStudentRepository studentRepository, IStudentSubjectRepository studentSubjectRepository)
        {
            _studentSubjectRepository = studentSubjectRepository;
            _studentRepository = studentRepository;
            _questionRepository = questionRepository;
            _resultRepository = resultRepository;
            _studentExamRepository = studentExamRepository;
            _examRepository = examRepository;
            _subjectRepository = subjectRepository;
            _subAdminRepository = subAdminRepository;
            _getCurrentUser = getCurrentUser;
        }

        public async Task<BaseResponse<CreateExamResponseModel>> StartExamAsync(CreateExamRequestModel model)
        {

            var userId = _getCurrentUser.GetCurrentUserId(); 
            if (userId == null)
            {
                return new BaseResponse<CreateExamResponseModel>()
                {
                    Message = "Session Expired Pls Login Again",
                    Status = false,
                };
            }


            var subAdmin = await _subAdminRepository.Get(a => a.UserId == userId);
            var subject = await _subjectRepository.Get(a => a.SubAdminId == subAdmin.Id && a.SubjectName == model.SubjectName);
            var students = subject.StudentSubjects.FirstOrDefault(a => a.SubjectId == subject.Id);
            var questions = subject.Questions;
            var ongoingExam = subject.Exams.FirstOrDefault(a => a.Ongoing == true);
            if (ongoingExam != null)
            {
                return new BaseResponse<CreateExamResponseModel>
                {
                    Message = "An Exam is still Valid for this Subject",
                    Status = false,
                };
            }
            if (students == null)
            {
                return new BaseResponse<CreateExamResponseModel>
                {
                    Message = "No Student is Assigned to the Subject",
                    Status = false,
                };
            }
            if (questions.Count < model.MaxQuestion || questions.Count == 0)
            {
                return new BaseResponse<CreateExamResponseModel>
                {
                    Message = "Questions are not Enough to Start Exam",
                    Status = false,
                };
            }
            var exam = new Exam
            {
                MaxQuestion = model.MaxQuestion,
                Title = model.Title,
                SubAdminId = subAdmin.Id,
                SubAdmin = subAdmin,
                SubjectId = subject.Id,
                Subject = subject,
                DurationMinutes = model.DurationMinutes,
                TimeScheduled = model.TimeScheduled,
            };
            subAdmin.Exams.Add(exam);
            subject.Exams.Add(exam);
            await _examRepository.Create(exam);
            await _examRepository.Save();
            return new BaseResponse<CreateExamResponseModel>
            {
                Message = "Exam Started",
                Status = true,
            };
        }

        public async Task<BaseResponse<EndExamResponseModel>> EndExamAsync(string id)
        {
            var userId = _getCurrentUser.GetCurrentUserId();
            var exam = await _examRepository.Get(id);
            var subject = exam.Subject;
            var subAdmin = await _subAdminRepository.Get(a => a.UserId == userId);
            var students = subAdmin.Students.Where(a => a.StudentExams.Any(b => b.ExamId != id));
            ICollection<StudentDto> absentStudents = [];
            absentStudents = students.Select(a => new StudentDto
            {
                FullName = $"{a.User.FirstName} {a.User.LastName}",
                Email = a.User.Email,
                SerialNumber = a.SerialNumber,
            }).ToList();
            foreach (var student in students)
            {
                var result = new Result
                {
                    Exam = exam,
                    ExamId = exam.Id,
                    Student = student,
                    StudentId = student.Id,
                    Subject = subject,
                    SubjectId = subject.Id,
                    Score = 0,
                };

                var studentExams = new StudentExam
                {
                    Exam = exam,
                    ExamId = exam.Id,
                    Student = student,
                    StudentId = student.Id
                };
                exam.Ongoing = false;
                student.Results.Add(result);
                student.StudentExams.Add(studentExams);
                exam.StudentExams.Add(studentExams);
                _examRepository.Update(exam);
                await _resultRepository.Create(result);
                await _studentExamRepository.Create(studentExams);
                await _examRepository.Save();
            }
            if (absentStudents != null)
            {
                return new BaseResponse<EndExamResponseModel>
                {
                    Message = "Exam Ended",
                    Status = true,
                    Data = new EndExamResponseModel
                    {
                        Students = absentStudents
                    }
                };
            }
            return new BaseResponse<EndExamResponseModel>
            {
                Message = "Exam Ended",
                Status = true,
            };
        }

        public async Task<BaseResponse<ICollection<LoadExamsDto>>> LoadAvailableExamAsync()
        {
            var userId = _getCurrentUser.GetCurrentUserId();
            var student = await _studentRepository.Get(a => a.UserId == userId);
            var availableExams = student.StudentSubjects.SelectMany(a => a.Subject.Exams).Where(a => a.Ongoing == true).ToList();
            var exams = availableExams.Select(a => new LoadExamsDto
            {
                ExamId = a.Id,
                SubjectName = a.Subject.SubjectName,
            }).ToList();
            return new BaseResponse<ICollection<LoadExamsDto>>
            {
                Status = true,
                Data = exams
            };
        }
        public async Task<BaseResponse<ICollection<ExamDto>>> GetAllExamsAsync()
        {
            var userId = _getCurrentUser.GetCurrentUserId();
            if (userId == null)
            {
                return new BaseResponse<ICollection<ExamDto>>()
                {
                    Message = "Session Expired Pls Login Again",
                    Status = false,
                };
            }
            var exams = await _examRepository.GetAll(a => a.SubAdmin.UserId == userId);

            var listOfExams = exams.Select(b => new ExamDto
            {
                Id = b.Id,
                Ongoing = b.Ongoing,
                DateCreated = b.DateCreated,
                SubjectName = b.Subject.SubjectName,
                Students = [.. b.StudentExams.Select(a => new StudentDto
                {
                    Email = a.Student.User.Email,
                    FullName = $"{a.Student.User.FirstName} {a.Student.User.LastName}",
                    SerialNumber = a.Student.SerialNumber
                }

                )]
            }).ToList();
            return new BaseResponse<ICollection<ExamDto>>()
            {
                Message = "Success",
                Status = true,
                Data = listOfExams
            };
        }
         public async Task<BaseResponse<SubmitExamDto>> SubmitExam(ExamSubmissionRequestModel model)
        {
            var userId = _getCurrentUser.GetCurrentUserId();
            int score = 0;
            var answerDict = model.Answers.ToDictionary(a => a.QuestionId, a => a.SelectedOption);
            Dictionary<string, string> review = [];
            foreach (var item in answerDict)
            {
                var question = await _questionRepository.Get(a => a.Id == item.Key);
                var answer = question.Answers.FirstOrDefault(a => a.IsCorrect == true)!.Label;
                if (answer == item.Value)
                {
                    score++;
                    review.Add(question.Text, "correct");
                }
                else if (item.Value == "No answer") review.Add(question.Text, "No answer");
                else review.Add(question.Text, "Wrong");
            }
             
            var student = await _studentRepository.Get(a => a.User.Id == userId);
            var exam = await _examRepository.Get(model.ExamId);
            var subject = exam.Subject;
            var result = new Result
            {
                Score = score,
                Exam = exam,
                ExamId = exam.Id,
                Student = student,
                StudentId = student.Id,
                Subject = exam.Subject,
                SubjectId = exam.Subject.Id,
            };
            var studentExams = new StudentExam
            {
                Exam = exam,
                ExamId = exam.Id,
                Student = student,
                StudentId = student.Id
            };
            student.Results.Add(result);
            student.StudentExams.Add(studentExams);
            exam.StudentExams.Add(studentExams);
            _examRepository.Update(exam);
            student.Results.Add(result);
            subject.Results.Add(result);
            exam.Results.Add(result);

            await _resultRepository.Create(result);
            await _resultRepository.Save();
            return new BaseResponse<SubmitExamDto>()
            {
                Status = true,
                Data = new SubmitExamDto
                {
                    Review = review,
                    score = score
                }
            };
        }
    }
}
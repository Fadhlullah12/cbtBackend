using System.Text.RegularExpressions;
using System.Threading.Tasks;
using cbtBackend.Dtos.RequestModels;
using cbtBackend.Dtos.ResponseModels;
using cbtBackend.Model;
using cbtBackend.Model.Entities;
using cbtBackend.Repositories.Interfaces;
using cbtBackend.Services.Interfaces;

namespace cbtBackend.Services.Implementations
{
    public class SubjectService : BaseResponse<Subject>, ISubjectService
    {
        IStudentSubjectRepository _studentSubjectRepository;
        ISubAdminRepository _subAdminRepository;
        IGetCurrentUser _currentUser;
        ISubjectRepository _subjectRepository;
        IAnswerRepository _answerRepository;
        IQuestionRepository _questionRepository;
        public SubjectService(IGetCurrentUser currentUser, ISubAdminRepository subAdminRepository, ISubjectRepository subjectRepository, IQuestionRepository questionRepository, IAnswerRepository answerRepository, IStudentSubjectRepository studentSubjectRepository)
        {
            _studentSubjectRepository = studentSubjectRepository;
            _answerRepository = answerRepository;
            _questionRepository = questionRepository;
            _subAdminRepository = subAdminRepository;
            _subjectRepository = subjectRepository;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<CreateSubjectResponseModel>> CreateSubjectAsync(CreateSubjectRequestModel model)
        {
            var userId = _currentUser.GetCurrentUserId();
            var subAdmin = await _subAdminRepository.Get(a => a.UserId == userId);
            var existingSubject = subAdmin.Subjects.FirstOrDefault(a => a.SubjectName == model.SubjectName && a.IsDeleted == false);
            if (existingSubject != null)
            {
                return new BaseResponse<CreateSubjectResponseModel>
                {
                    Status = false,
                    Message = "Sorry this Subject already exists",
                };
            }
            if (model.QuestionFile != null)
            {
                var subject = new Subject
                {
                    SubjectName = model.SubjectName,
                    SubAdmin = subAdmin,
                    SubAdminId = subAdmin.Id,
                };
                var reader = new StreamReader(model.QuestionFile.OpenReadStream());
                var rawText = await reader.ReadToEndAsync();
                var questions = await ParseRawQuestions(rawText, subject);
                subject.Questions = questions;
                await _subjectRepository.Create(subject);
                await _subjectRepository.Save();
                return new BaseResponse<CreateSubjectResponseModel>
                {
                    Status = true,
                    Message = "Subject Created and Questions Uploaded Successfully",
                    Data = new CreateSubjectResponseModel
                    {
                        SubjectName = subject.SubjectName,
                    }
                };
            }
            var subject2 = new Subject
            {
                SubjectName = model.SubjectName,
                SubAdmin = subAdmin,
                SubAdminId = subAdmin.Id,
            };
            subAdmin.Subjects.Add(subject2);
            await _subjectRepository.Create(subject2);
            await _subjectRepository.Save();
            return new BaseResponse<CreateSubjectResponseModel>
            {
                Status = true,
                Message = "Subject Created Successfully",
                Data = new CreateSubjectResponseModel
                {
                    SubjectName = subject2.SubjectName
                }
            };
        }

        public async Task<BaseResponse<ICollection<SubjectDto>>> ViewAllSubjectAsync()
        {
            var userId = _currentUser.GetCurrentUserId();
            if (userId == null)
            {
                return new BaseResponse<ICollection<SubjectDto>>()
                {
                    Message = "Session Expired Pls Login Again",
                    Status = false,
                };
            }
            var subjects = await _subjectRepository.GetAll(a => a.SubAdmin.UserId == userId && a.IsDeleted == false);

            var listOfSubjects = subjects.Select(a => new SubjectDto
            {
                Id = a.Id,
                SubjectName = a.SubjectName,
                SubjectExams = a.Exams.Count,
                SubjectStudents = a.StudentSubjects.Count
            
            }).ToList();
            return new BaseResponse<ICollection<SubjectDto>>()
            {
                Message = "success",
                Status = true,
                Data = listOfSubjects
            };
        }

        public async Task<BaseResponse<ICollection<StudentDto>>> ViewAllSubjectStudentAsync(string subjectId)
        {
            var studentSubjects = await _studentSubjectRepository.GetSubjectsAsync(subjectId);
            var listOfStudents = studentSubjects.Select(a => new StudentDto
            {
                Id = a.Id,
                FullName = $"{a.Student.User.FirstName} {a.Student.User.LastName}",
                Email = a.Student.User.Email,
                SerialNumber = a.Student.SerialNumber,
                Subjects = a.Student.StudentSubjects.Count
            }).ToList();
            return new BaseResponse<ICollection<StudentDto>>()
            {
                Message = "Success",
                Status = true,
                Data = listOfStudents
            };
        }

     
        public async Task<bool> UploadSubjectQuestionsAsync(UploadQuestionRequestModel model)
        {
            var subject = await _subjectRepository.Get(model.Id);
            var reader = new StreamReader(model.QuestionFile.OpenReadStream());
            var rawText = await reader.ReadToEndAsync();
            var questions = await ParseRawQuestions(rawText, subject);
            subject.Questions = questions;
            _subjectRepository.Update(subject);
            await _subjectRepository.Save();
            return true;
        }
        
         public async Task<BaseResponse<SubjectDto>> Update(UpdateSubjectRequestModel model)
        {
            var userId = _currentUser.GetCurrentUserId();
            var subAdmin = await _subAdminRepository.Get(a => a.UserId == userId);
            var subject = await _subjectRepository.Get(model.Id);
            if (subject == null)
            {
                return new BaseResponse<SubjectDto>
                {
                    Message = "Subject not found.",
                    Status = false
                };
            }

            if (subject.SubjectName.Equals(model.SubjectName, StringComparison.OrdinalIgnoreCase))
            {
                return new BaseResponse<SubjectDto>
                {
                    Message = "No changes detected. Subject name remains the same.",
                    Status = true,
                    Data = new SubjectDto
                    {
                        SubjectName = subject.SubjectName
                    }
                };
            }

            var existingSubject =  subAdmin.Subjects.FirstOrDefault(a => a.SubjectName == model.SubjectName && a.Id != model.Id);
            if (existingSubject != null)
            {
                return new BaseResponse<SubjectDto>
                {
                    Message = "A subject with this name already exists.",
                    Status = false
                };
            }

            subject.SubjectName = model.SubjectName;
            _subjectRepository.Update(subject);
            await _subjectRepository.Save();

            return new BaseResponse<SubjectDto>
            {
                Message = "Updated successfully.",
                Status = true,
                Data = new SubjectDto
                {
                    SubjectName = subject.SubjectName
                }
            };


        }

         public async Task<BaseResponse<bool>> Delete(string Id)
        {
            var subject = await _subjectRepository.Get(Id);
            subject.IsDeleted = true;
            if (subject.StudentSubjects.Count > 0)
            {
                foreach (var item in subject.StudentSubjects)
                {
                    item.IsDeleted = true;
                }
            }
            _subjectRepository.Update(subject);
            await _subjectRepository.Save();
            return new BaseResponse<bool>
            {
                Message = "Deleted Succesfully",
                Status = true,
            };
        }

        private async Task<List<Question>> ParseRawQuestions(string rawInput, Subject subject)
        {
            var questions = new List<Question>();
            var blocks = Regex.Split(rawInput.Trim(), @"(?=\d+\.)"); // Split by question number 

            foreach (var block in blocks)
            {
                if (string.IsNullOrWhiteSpace(block)) continue;

                var lines = block.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                var questionLine = lines.FirstOrDefault(); if (questionLine == null || !Regex.IsMatch(questionLine, @"^\d+\.")) continue;
                var questionText = questionLine.Substring(questionLine.IndexOf('.') + 1).Trim();
                var answers = new List<Answer>();
                string? correctLabel = null;
                var question = new Question
                {
                    Text = questionText,
                    Subject = subject,
                    SubjectId = subject.Id,
                };
                foreach (var line in lines.Skip(1))
                {
                    {
                        var match = Regex.Match(line, @"Answer\s*[:\-]?\s*([A-Da-d])", RegexOptions.IgnoreCase);
                        if (match.Success)
                        {
                            correctLabel = match.Groups[1].Value.ToUpper();
                            continue;
                        }
                    }
                    {
                        var label = line.Substring(0, 1).ToUpper();
                        var content = line.Substring(2).Trim();
                        var answer = new Answer
                        {
                            Label = content,
                            IsCorrect = false,
                            Question = question,
                            QuestionId = question.Id
                        };
                        await _answerRepository.Create(answer);
                        answers.Add(answer);
                    }
                }
                if (correctLabel == null)
                {
                    continue;
                }
                // Mark correct answer             if (correctLabel != null && answers.Count >= 1) 
                var index = correctLabel[0] - 'A';
                if (index >= 0 && index < answers.Count)
                {
                    answers[index].IsCorrect = true;
                }

                question.Answers = answers;
                await _questionRepository.Create(question);
                questions.Add(question);

            }

            return questions;
        }


    }
}


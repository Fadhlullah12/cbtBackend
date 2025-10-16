using cbtBackend.Dtos.RequestModels;
using cbtBackend.Dtos.ResponseModels;
using cbtBackend.Model;
using cbtBackend.Repositories.Interfaces;
using cbtBackend.Services.Interfaces;

namespace cbtBackend.Services.Implementations
{
    public class StudentService : BaseResponse<Student>, IStudentService
    {
        IStudentExamRepository _studentExamRepository;
        ISubjectRepository _subjectRepository;
        IUserRepository _userRepository;
        ISubAdminRepository _subAdminRepository;
        IStudentRepository _studentRepository;
        IGetCurrentUser _getCurrentUser;
        IStudentSubjectRepository _studentSubjectRepository;
        IResultRepository _resultRepository;
        public StudentService(IUserRepository userRepository, IStudentRepository studentRepository, IGetCurrentUser getCurrentUser, ISubAdminRepository subAdminRepository, IStudentSubjectRepository studentSubjectRepository, IStudentExamRepository studentExamRepository, ISubjectRepository subjectRepository, IResultRepository resultRepository)
        {
            _resultRepository = resultRepository;
            _subjectRepository = subjectRepository;
            _studentExamRepository = studentExamRepository;
            _studentSubjectRepository = studentSubjectRepository;
            _subAdminRepository = subAdminRepository;
            _getCurrentUser = getCurrentUser;
            _userRepository = userRepository;
            _studentRepository = studentRepository;
        }

        public async Task<BaseResponse<CreateStudentResponseModel>> RegisterStudent(CreateStudentRequestModel model)
        {
            var existingUser = await _userRepository.Get(a => a.Email == model.Email);
            var userId = _getCurrentUser.GetCurrentUserId();
            var subAdmin = await _subAdminRepository.Get(a => a.UserId == userId);
            if (existingUser != null)
            {
                return new BaseResponse<CreateStudentResponseModel>
                {
                    Message = $"Sorry User with Email {existingUser.Email} already exists",
                    Status = false,
                };
            }
            var user = new User
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Role = "Student",
            };

            var student = new Student
            {
                User = user,
                UserId = user.Id,
                SubAdmin = subAdmin,
                SubAdminId = subAdmin.Id,
                SerialNumber = $"{subAdmin.User.FirstName[0]}{subAdmin.User.FirstName[1]}{subAdmin.User.FirstName[2]}{new Random().Next(1000)}"
            };
            subAdmin.Students.Add(student);
            user.Student = student;
            user.Password = BCrypt.Net.BCrypt.HashPassword(student.SerialNumber);
            await _studentRepository.Create(student);
            await _userRepository.Create(user);
            await _studentRepository.Save();
            return new BaseResponse<CreateStudentResponseModel>
            {
                Message = "Student Registered Sucessfully",
                Status = true,
                Data = new CreateStudentResponseModel
                {
                    Email = user.Email,
                    FullName = $"{user.FirstName} {user.LastName}"
                }
            };
        }

        public async Task<bool> AssignSubjects(AssignSubjectsRequestModel model)
        {
            var student = await _studentRepository.Get(model.StudentId);
            bool exist = false;
            foreach (var subjectId in model.SubjectIds)
            {
                var exists = student.StudentSubjects.FirstOrDefault(a => a.SubjectId == subjectId);
                if (exists != null)
                {
                    exist = true;
                    continue;
                }
                var subject = await _subjectRepository.Get(subjectId);
                var studentSubject = new StudentSubject
                {
                    Student = student,
                    StudentId = student.Id,
                    Subject = subject,
                    SubjectId = subject.Id
                };
                await _studentSubjectRepository.Create(studentSubject);
                subject.StudentSubjects.Add(studentSubject);
                student.StudentSubjects.Add(studentSubject);
            }
            if (exist == true)
            {
                return false;
            }
            await _studentRepository.Save();
            return true;
        }
        public async Task<BaseResponse<CreateMultipleStudentResponseModel>> RegisterStudents(IEnumerable<CreateStudentRequestModel> models)
        {
            ICollection<CreateStudentResponseModel>? unRegisteredStudents = [];
            ICollection<CreateStudentResponseModel>? registeredStudents = [];
            var userId = _getCurrentUser.GetCurrentUserId();
            if (userId == null)
            {
                return new BaseResponse<CreateMultipleStudentResponseModel>()
                {
                    Message = "Session Expired Pls Login Again",
                    Status = false,
                };
            }
            var subAdmin = await _subAdminRepository.Get(a => a.UserId == userId);
            foreach (var item in models)
            {
                var existingUser = await _userRepository.Get(a => a.Email == item.Email);
                if (existingUser != null)
                {
                    var UnSucessfullyRegistered = new CreateStudentResponseModel
                    {
                        Email = item.Email,
                        FullName = $"{item.FirstName} {item.LastName}",
                    };
                    unRegisteredStudents.Add(UnSucessfullyRegistered);
                    continue;
                }
                var user = new User
                {
                    Email = item.Email,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Role = "Student",
                };

                var student = new Student
                {
                    User = user,
                    UserId = user.Id,
                    SubAdmin = subAdmin,
                    SubAdminId = subAdmin.Id,
                    SerialNumber = $"{subAdmin.User.FirstName[0]}{subAdmin.User.FirstName[1]}{subAdmin.User.FirstName[2]}{new Random().Next(1000)}"
                };
                var sucessfullyRegistered = new CreateStudentResponseModel
                {
                    Email = user.Email,
                    FullName = $"{user.FirstName} {user.LastName}",
                    SerialNumber = student.SerialNumber
                };
                user.Password = BCrypt.Net.BCrypt.HashPassword(student.SerialNumber);
                user.Student = student;
                subAdmin.Students.Add(student);
                registeredStudents.Add(sucessfullyRegistered);
                await _studentRepository.Create(student);
                await _userRepository.Create(user);
                await _studentRepository.Save();
            }
            return new BaseResponse<CreateMultipleStudentResponseModel>
            {
                Status = false,
                Data = new CreateMultipleStudentResponseModel
                {
                    registeredStudents = registeredStudents,
                    unRegisteredStudents = unRegisteredStudents
                }
            };
        }
        public async Task<BaseResponse<ICollection<StudentDto>>> GetAllStudentsAsync()
        {
            var userId = _getCurrentUser.GetCurrentUserId();
            if (userId == null)
            {
                return new BaseResponse<ICollection<StudentDto>>()
                {
                    Message = "Session Expired Pls Login Again",
                    Status = false,
                };
            }
            var subAdmin = await _subAdminRepository.Get(a => a.UserId == userId);
            var students = await _studentRepository.GetAll(subAdmin.Id);

            var listOfStudents = students.Select(a => new StudentDto
            {
                Id = a.Id,
                FullName = $"{a.User.FirstName} {a.User.LastName}",
                Email = a.User.Email,
                SerialNumber = a.SerialNumber,
                Subjects = a.StudentSubjects.Count
            }).ToList();
            return new BaseResponse<ICollection<StudentDto>>()
            {
                Message = "Success",
                Status = true,
                Data = listOfStudents
            };
        }
        public async Task<BaseResponse<ICollection<ExamDto>>> ViewAllStudentExamsAsync(string studentId)
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
            var subAdmin = await _subAdminRepository.Get(a => a.UserId == userId);
            var studentExams = await _studentExamRepository.GetStudentsAsync(studentId);
            var listOfStudents = studentExams.Select(a => new ExamDto
            {
                Title = a.Exam.Title,
                DateCreated = a.Exam.DateCreated,
                SubjectName = a.Exam.Subject.SubjectName,
            }).ToList();
            return new BaseResponse<ICollection<ExamDto>>()
            {
                Message = "Success",
                Status = true,
                Data = listOfStudents
            };
        }
        public async Task<BaseResponse<ICollection<SubjectDto>>> ViewAllStudentSubjectAsync(string studentId)
        {
            var studentSubjects = await _studentSubjectRepository.GetStudentsAsync(studentId);
            var listOfSubjects = studentSubjects.Select(a => new SubjectDto
            {
                SubjectName = a.Subject.SubjectName
            }).ToList();
            return new BaseResponse<ICollection<SubjectDto>>()
            {
                Message = "Success",
                Status = true,
                Data = listOfSubjects
            };
        }

        public async Task<BaseResponse<StudentDto>> UpdateStudent(UpdateStudentRequestModel model)
        {
            var existingUser = await _userRepository.Get(a => a.Email == model.Email && a.Id != model.Id);
            if (existingUser != null)
            {
                return new BaseResponse<StudentDto>
                {
                    Message = "Sorry, a user with this email already exists.",
                    Status = false,
                };
            }

            var student = await _studentRepository.Get(a => a.Id == model.Id);
            if (student == null)
            {
                return new BaseResponse<StudentDto>
                {
                    Message = "Student not found.",
                    Status = false,
                };
            }



            student.User.Email = model.Email;
            student.User.FirstName = model.FirstName;
            student.User.LastName = model.LastName;
            student.User.Student = student;
            _studentRepository.Update(student);
            await _userRepository.Save(); // Or your actual unit of work

            return new BaseResponse<StudentDto>
            {
                Message = "Profile updated successfully.",
                Status = true,
                Data = new StudentDto
                {
                    Email = student.User.Email,
                    FullName = $"{student.User.FirstName} {student.User.LastName}",
                    SerialNumber = student.SerialNumber,
                }
            };

        }

        public async Task<BaseResponse<ICollection<ResultsDto>>> ViewStudentResultAsync(string studentId)
        {
            var exams = await _studentSubjectRepository.GetStudentsAsync(studentId);

            var listOfResults = exams.Select(a => new ResultsDto
            {
                Id = a.Id,
                SubjectName = a.Subject.SubjectName,
                NoOfResults = a.Student.Results
                    .Where(r => r.Exam.SubjectId == a.Subject.Id)
                    .Count(),

                ProfileResults = a.Student.Results
                    .Where(r => r.Exam.SubjectId == a.Subject.Id)
                    .Select(b => new ProfileResultsDto
                    {
                        Title = b.Exam.Title,
                        Score = b.Score,
                        Questions = b.Exam.MaxQuestion,
                        DateCreated = b.DateCreated.ToString("yyyy-MM-dd")
                    }).ToList()
            }).ToList();

            return new BaseResponse<ICollection<ResultsDto>>()
            {
                Message = "Success",
                Status = true,
                Data = listOfResults
            };
        }


        public async Task<BaseResponse<ICollection<AllStudentsResultsdto>>> ViewStudentsResultAsync()
        {
            var userId = _getCurrentUser.GetCurrentUserId();
            var students = await _studentRepository.GetAll(a => a.SubAdmin.UserId == userId && a.IsDeleted == false);
            var listOfStudentsResults = students.Select(a => new AllStudentsResultsdto
            {
                FullName = $"{a.User.FirstName} {a.User.LastName}",
                Results = [.. a.Results.Select(r => new MultipleStudentResultDto
                {
                    Title = r.Exam.Title,
                    Questions = r.Exam.MaxQuestion,
                    SubjectName = r.Subject.SubjectName,
                    Score = r.Score
                })]
            }).ToList();
            return new BaseResponse<ICollection<AllStudentsResultsdto>>()
            {
                Message = "Success",
                Status = true,
                Data = listOfStudentsResults
            };
        }

        public async Task<bool> Delete(string id)
        {
            var student = await _studentRepository.Get(id);
            if (student == null)
            {
                return false;
            }
            student.IsDeleted = true;
            if (student.Results.Count > 0)
            {
                foreach (var item in student.Results)
                {
                    item.IsDeleted = true;
                }
            }
            if (student.StudentSubjects.Count > 0)
            {
                foreach (var item in student.StudentSubjects)
                {
                    item.IsDeleted = true;
                }
            }
            if (student.StudentExams.Count > 0)
            {
                foreach (var item in student.StudentExams)
                {
                    item.IsDeleted = true;
                }
            }
            await _studentRepository.Save();
            return true;
        }
        
          public async Task<bool> DeleteStudentSubject(string id)
        {
            var studentSubject = await _studentSubjectRepository.Get(id);
            studentSubject.IsDeleted = true;
            await _studentRepository.Save();
            return true;
        }

    }
}
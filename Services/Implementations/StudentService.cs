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
        public StudentService(IUserRepository userRepository, IStudentRepository studentRepository, IGetCurrentUser getCurrentUser, ISubAdminRepository subAdminRepository, IStudentSubjectRepository studentSubjectRepository, IStudentExamRepository studentExamRepository, ISubjectRepository subjectRepository)
        {
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
            foreach (var subjectId in model.SubjectIds)
            {
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
            var students = subAdmin.Students.Where(a => a.IsDeleted == false);

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
                Ongoing = a.Exam.Ongoing,
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
            var user = await _userRepository.Get(a => a.Email == model.Email && a.Id != model.Id);
            var student = await _studentRepository.Get(a => a.UserId == user.Id);
            if (user == null)
            {
                return new BaseResponse<StudentDto>
                {
                    Message = $"Sorry user with Email already exists",
                    Status = false,
                };
            }

            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            student.User = user;
            student.UserId = user.Id;
            user.Student = student;
            _studentRepository.Update(student);
            _userRepository.Update(user);
            await _subAdminRepository.Save();
            return new BaseResponse<StudentDto>
            {
                Message = "Student Profile Updated Sucessfully",
                Status = true,
                Data = new StudentDto
                {
                    Email = user.Email,
                    FullName = $"{user.FirstName} {user.LastName}",
                    SerialNumber = student.SerialNumber,
                }
            };
        }

        public async Task<BaseResponse<ICollection<AllStudentsResultsdto>>> ViewStudentsResultAsync()
        {
            var userId = _getCurrentUser.GetCurrentUserId();
            var subAdmin = await _subAdminRepository.Get(a => a.UserId == userId);
            var students = subAdmin.Students.Where(a => a.IsDeleted == false)
            .Select(s => new AllStudentsResultsdto
            {
                FullName = $"{s.User.FirstName} {s.User.LastName}",
                Results = [.. s.Results.Select(r => new MultipleStudentResultDto
                {
                    Title = r.Exam.Title,
                    Score = r.Score,
                    SubjectName = r.Subject.SubjectName,
                    Questions = r.Exam.MaxQuestion,
                    Date = r.DateCreated.ToString()

                })]
            }).ToList();

            return new BaseResponse<ICollection<AllStudentsResultsdto>>
            {
                Data = students,
                Status = true
            };

        }

        public async Task<BaseResponse<ICollection<StudentResult>>> ViewStudentResultAsync(string Id)
        {
            var student = await _studentRepository.Get(Id);
            var studentResults = student.StudentSubjects.Where(a => a.IsDeleted == false).Select(a => new StudentResult
            {
                Id = a.Id,
                SubjectName = a.Subject.SubjectName,
                ProfileResults = [.. a.Subject.Results.Where(s => s.StudentId == a.StudentId).Select(r => new ProfileResults
                {
                    Title = r.Exam.Title,
                    Questions = r.Exam.MaxQuestion,
                    Date = r.DateCreated.ToString(),
                    Score = r.Score
                })]
            }).ToList();

            return new BaseResponse<ICollection<StudentResult>>
            {
                Data = studentResults,
                Status = true
            };
        }

        public async Task<bool> Delete(string Id)
        {
            var student = await _studentRepository.Get(Id);
            student.IsDeleted = true;
            student.User.IsDeleted = true;
            foreach (var result in student.Results)
            {
                result.IsDeleted = true;
            }

            foreach (var exam in student.StudentExams)
            {
                exam.IsDeleted = true;
            }
            foreach (var subject in student.StudentSubjects)
            {
                subject.IsDeleted = true;
            }
            await _studentRepository.Save();

            return true;
        }
        
        public async Task<bool> DeleteStudentSubject(string Id)
        {
            var subject = await _studentSubjectRepository.GetSubject(Id);
            subject.IsDeleted = true;
            await _studentRepository.Save();
            return true;
        }
    }
}
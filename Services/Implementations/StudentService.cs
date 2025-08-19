using cbtBackend.Dtos.RequestModels;
using cbtBackend.Dtos.ResponseModels;
using cbtBackend.Model;
using cbtBackend.Repositories.Interfaces;
using cbtBackend.Services.Interfaces;

namespace cbtBackend.Services.Implementations
{
    public class StudentService : BaseResponse<Student>, IStudentService
    {
        IUserRepository _userRepository;
        IStudentRepository _studentRepository;
        public StudentService(IUserRepository userRepository, IStudentRepository studentRepository)
        {
            _userRepository = userRepository;
            _studentRepository = studentRepository;
        }

        public async Task<BaseResponse<CreateStudentResponseModel>> RegisterStudent(CreateStudentRequestModel model)
        {
            var existingUser = await _userRepository.Get(a => a.Email == model.Email);
            if (existingUser != null)
            {
                return new BaseResponse<CreateStudentResponseModel>
                {
                    Message = $"Sorry Student with Email {existingUser.Email} already exists",
                    Status = false,
                };
            }
            var user = new User
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Role = "Student",
            };

            var student = new Student
            {
                User = user,
                UserId = user.Id,
            };
            user.Student = student;
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
        public async Task<BaseResponse<CreateMultipleStudentResponseModel>> RegisterStudents(IEnumerable<CreateStudentRequestModel> models)
        {
            ICollection<CreateStudentResponseModel>? unRegisteredStudents = [];
            ICollection<CreateStudentResponseModel>? registeredStudents = [];
            foreach (var item in models)
            {
                var existingUser = await _userRepository.Get(a => a.Email == item.Email);
                if (existingUser != null)
                {
                    var UnSucessfullyRegistered = new CreateStudentResponseModel
                    {
                        Email = item.Email,
                        FullName = $"{item.FirstName} {item.LastName}",
                        SerialNumber = item.SerialNumber
                    };
                }
            }


            foreach (var item in models)
            {
                var user = new User
                {
                    Email = item.Email,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Password = BCrypt.Net.BCrypt.HashPassword(item.Password),
                    Role = "Student",
                };

                var student = new Student
                {
                    User = user,
                    UserId = user.Id,
                };
                var sucessfullyRegistered = new CreateStudentResponseModel
                {
                    Email = user.Email,
                    FullName = $"{user.FirstName} {user.LastName}",
                    SerialNumber = student.SerialNumber
                };
                user.Student = student;
                registeredStudents.Add(sucessfullyRegistered);
                await _studentRepository.Create(student);
                await _userRepository.Create(user);
                await _studentRepository.Save();
                return new BaseResponse<CreateMultipleStudentResponseModel>
                {
                    Data = new CreateMultipleStudentResponseModel
                    {
                        registeredStudents = registeredStudents,
                        unRegisteredStudents = unRegisteredStudents
                    }
                };
            }
             return new BaseResponse<CreateMultipleStudentResponseModel>
                {
                    Message = "No input Found",
                    Status = false,                  
                }; 
        }
    }
}
using cbtBackend.Dtos.RequestModels;
using cbtBackend.Dtos.ResponseModels;
using cbtBackend.Model;
using cbtBackend.Model.Enums;
using cbtBackend.Repositories.Interfaces;
using cbtBackend.Services.Interfaces;

namespace cbtBackend.Services.Implementations
{
    public class SubAdminService : BaseResponse<SubAdmin>, ISubAdminService
    {
        IGetCurrentUser _getCurrentUser;
        IMailService _mailService;
        ISubAdminRepository _subAdminRepository;
        IUserRepository _userRepository;
        public SubAdminService(IUserRepository userRepository, ISubAdminRepository subAdminRepository, IGetCurrentUser getCurrentUser,IMailService mailService)
        {
            _mailService = mailService;
            _getCurrentUser = getCurrentUser;
            _userRepository = userRepository;
            _subAdminRepository = subAdminRepository;
        }

        public async Task<bool> ApproveSubAdminAsync(string id)
        {

            var subAdmin = await _subAdminRepository.Get(a => a.Id == id);
            if (subAdmin == null)
            {
                return false;
            }
            subAdmin.ApprovalStatus = ApprovalStatus.Approved;
            await _mailService.SendEmailAfterSubAdminApprovalAsync($"{subAdmin.User.Email}", $"{subAdmin.User.FirstName} {subAdmin.User.LastName}",true);
            await _subAdminRepository.Save();
            return true;

        }
        public async Task<bool> RejectSubAdminAsync(string id)
        {

            var subAdmin = await _subAdminRepository.Get(a => a.Id == id);
            if (subAdmin == null)
            {
                return false;
            }
            subAdmin.ApprovalStatus = ApprovalStatus.Rejected;
            _subAdminRepository.Update(subAdmin);
            await _mailService.SendEmailAfterSubAdminApprovalAsync($"{subAdmin.User.Email}", $"{subAdmin.User.FirstName} {subAdmin.User.LastName}", false);
            await _subAdminRepository.Save();
            return true;
        }

        public async Task<BaseResponse<CreateSubAdminResponseModel>> CreateSubAdminAsync(CreateSubAdminRequestModel model)
        {

            var existingUser = await _userRepository.Get(a => a.Email == model.Email);
            if (existingUser != null)
            {
                return new BaseResponse<CreateSubAdminResponseModel>
                {
                    Message = $"Sorry user with Email {existingUser.Email} already exists",
                    Status = false,
                };
            }
            var user = new User
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Role = "SubAdmin",
            };
            var subAdmin = new SubAdmin
            {
                User = user,
                UserId = user.Id,
            };
            user.SubAdmin = subAdmin;
            await _userRepository.Create(user);
            await _subAdminRepository.Create(subAdmin);
            await _subAdminRepository.Save();
            return new BaseResponse<CreateSubAdminResponseModel>
            {
                Message = "Registeration Submitted. Awaiting Approval",
                Status = true,
                Data = new CreateSubAdminResponseModel
                {
                    Email = user.Email,
                    UserName = $"{user.FirstName} {user.LastName}"
                }
            };
        }
        public async Task<BaseResponse<CreateSubAdminResponseModel>> UpdateSubAdminAsync(CreateSubAdminRequestModel model)
        {
            var userId = _getCurrentUser.GetCurrentUserId();
            var existingUser = await _userRepository.Get(a => a.Email == model.Email && a.Id != userId);
            var subAdmin = await _subAdminRepository.Get(a => a.UserId == userId);
            if (existingUser != null)
            {
                return new BaseResponse<CreateSubAdminResponseModel>
                {
                    Message = $"Sorry user with Email already exists",
                    Status = false,
                };
            }
             var user = await _userRepository.Get(a => a.Id == userId);
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            subAdmin.User = user;
            subAdmin.UserId = user.Id;
            user.SubAdmin = subAdmin;
            _subAdminRepository.Update(subAdmin);
            _userRepository.Update(user);
            await _subAdminRepository.Save();
            return new BaseResponse<CreateSubAdminResponseModel>
            {
                Message = "Profile Updated Sucessfully",
                Status = true,
                Data = new CreateSubAdminResponseModel
                {
                    Email = user.Email,
                    UserName = $"{user.FirstName} {user.LastName}"
                }
            };
        }

        public async Task<BaseResponse<ICollection<SubAdminDto>>> GetAllSubAdminAsync()
        {
            var subAdmin = await _subAdminRepository.GetAll(a => a.ApprovalStatus == ApprovalStatus.Approved);

            var listOfSubAdmin = subAdmin.Select(a => new SubAdminDto
            {
                FullName = $"{a.User.FirstName} {a.User.LastName}",
                Email = a.User.Email,
                NoOfStudents = a.Students.Count,
                RegisterationDate = a.DateCreated.Date.ToString(),
            }).ToList();
            return new BaseResponse<ICollection<SubAdminDto>>()
            {
                Message = "Success",
                Status = true,
                Data = listOfSubAdmin
            };
        }
        public async Task<BaseResponse<ICollection<SubAdminDto>>> GetAllUnApprovedAsync()
        {
            var subAdmin = await _subAdminRepository.GetAll(a => a.ApprovalStatus == ApprovalStatus.Pending);
            if (subAdmin.Count == 0)
            {
                 return new BaseResponse<ICollection<SubAdminDto>>()
            {
                Message = "No SubAdmin found",
                Status = false,
            };
            }
            var listOfSubAdmin = subAdmin.Select(a => new SubAdminDto
            {
                Id = a.Id,
                FullName = $"{a.User.FirstName} {a.User.LastName}",
                Email = a.User.Email,
                RegisterationDate = a.DateCreated.Date.ToString()
            }).ToList();
            return new BaseResponse<ICollection<SubAdminDto>>()
            {
                Message = "Success",
                Status = true,
                Data = listOfSubAdmin
            };
        }
    }
}
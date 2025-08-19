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
        ISubAdminRepository _subAdminRepository;
        IUserRepository _userRepository;
        public SubAdminService(IUserRepository userRepository, ISubAdminRepository subAdminRepository)
        {
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
            await _subAdminRepository.Create(subAdmin);
            await _userRepository.Create(user);
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
    }
}
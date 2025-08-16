using cbtBackend.Dtos.RequestModels;
using cbtBackend.Dtos.ResponseModels;
using cbtBackend.Model;
using cbtBackend.Repositories.Interfaces;
using cbtBackend.Services.Interfaces;

namespace cbtBackend.Services.Implementations
{
    public class SubAdminService : BaseResponse<SubAdmin>, ISubAdminService
    {
        IUserRepository _userRepository;
        public SubAdminService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseResponse<CreateSubAdminResponseModel>> CreateSubAdminAsync(CreateSubAdminRequestModel model)
        {
            
            var existingUser = await _userRepository.Get(a => a.Email == model.Email);
             if (existingUser != null)
            {
                return new BaseResponse<CreateSubAdminResponseModel>
                {
                    Message = $"Sorry user with Email {existingUser.Email}",
                    Status = false,
                };
            }
            var user = new User
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password,
                Role = "SubAdmin",
            };

            var subAdmin = new SubAdmin
            {
                User = user,
                UserId = user.Id,
            };
            user.SubAdmin = subAdmin;
            return new BaseResponse<CreateSubAdminResponseModel>
            {
                Message = "Registeration Pending",
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
using cbtBackend.Dtos.ResponseModels;
using cbtBackend.Dtos.RequestModels;
using cbtBackend.Services.Interfaces;
using cbtBackend.Repositories.Interfaces;
using cbtBackend.Model.Enums;
namespace cbtBackend.Services.Implementations
{
     public class LoginService : BaseResponse<LoginResponseModel>,ILoginService
    {
        IUserRepository _userRepository;
        public LoginService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseResponse<LoginResponseModel>> LoginAsync(LoginRequestModel model)
        {
            var existingUser = await _userRepository.Get(a => a.Email == model.Email);
             if (existingUser == null)
            {
                return new BaseResponse<LoginResponseModel>
                {
                    Message = $"Email {model.Email} not Found",
                    Status = false,
                };
            }
             if (existingUser.Role == "SubAdmin")
            {
               if (existingUser.SubAdmin.ApprovalStatus == ApprovalStatus.Rejected || existingUser.SubAdmin.ApprovalStatus == ApprovalStatus.Pending)
               {
                  return new BaseResponse<LoginResponseModel>
                {
                    Message = $"UnAuthourized Login Credentials",
                    Status = false,
                };
               }
            }
          if (BCrypt.Net.BCrypt.Verify(model.Password, existingUser.Password))
            {
                return new BaseResponse<LoginResponseModel>
                {
                    Message = "Login Successful",
                    Status = true,
                    Data = new LoginResponseModel
                    {
                        Role = existingUser.Role,
                        UserName = $"{existingUser.FirstName} {existingUser.LastName}"
                    }
                };
            }
            return new BaseResponse<LoginResponseModel>()
            {
                Status = false,
                Message = "Invalid Password",
                Data = null
            };
           
        }
    
    }
}
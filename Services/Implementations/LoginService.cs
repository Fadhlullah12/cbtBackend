using cbtBackend.Dtos.ResponseModels;
using cbtBackend.Dtos.RequestModels;
using cbtBackend.Services.Interfaces;
using cbtBackend.Repositories.Interfaces;
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
            var existingUser = await _userRepository.Get(a => a.Password == model.Password);
             if (existingUser == null)
            {
                return new BaseResponse<LoginResponseModel>
                {
                    Message = "Invalid Login Credentials",
                    Status = false,
                };
            }
          
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
    
    }
}
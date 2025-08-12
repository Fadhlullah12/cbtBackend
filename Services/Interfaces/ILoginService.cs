using cbtBackend.Dtos.ResponseModels;
using cbtBackend.Dtos.RequestModels;
namespace cbtBackend.Services.Interfaces
{
    public interface ILoginService
    {
        Task<BaseResponse<LoginResponseModel>> LoginAsync(LoginRequestModel model);
    }
}
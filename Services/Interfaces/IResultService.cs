using cbtBackend.Dtos.RequestModels;
using cbtBackend.Dtos.ResponseModels;

namespace cbtBackend.Services.Interfaces
{
    public interface IResultService
    {
        Task<BaseResponse<CreateResultResponseModel>> LoginAsync(CreateResultRequestModel model); 
    }
}
using cbtBackend.Dtos.RequestModels;
using cbtBackend.Dtos.ResponseModels;

namespace cbtBackend.Services.Interfaces
{
    public interface IExamResponseModel
    {
        Task<BaseResponse<CreateExamResponseModel>> LoginAsync(CreateExamRequestModel model);
    }
}
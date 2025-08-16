using cbtBackend.Dtos.RequestModels;
using cbtBackend.Dtos.ResponseModels;

namespace cbtBackend.Services.Interfaces
{
    public interface ISubjectService
    {
        Task<BaseResponse<CreateSubjectResponseModel>> LoginAsync(CreateSubjectRequestModel model);
    }
}
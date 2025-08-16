using cbtBackend.Dtos.RequestModels;
using cbtBackend.Dtos.ResponseModels;

namespace cbtBackend.Services.Interfaces
{
    public interface ISubAdminService
    {
        Task<BaseResponse<CreateSubAdminResponseModel>> CreateSubAdminAsync(CreateSubAdminRequestModel model);
    }
}
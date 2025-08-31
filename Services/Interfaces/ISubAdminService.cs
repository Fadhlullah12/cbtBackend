using cbtBackend.Dtos.RequestModels;
using cbtBackend.Dtos.ResponseModels;

namespace cbtBackend.Services.Interfaces
{
    public interface ISubAdminService
    {
        Task<BaseResponse<CreateSubAdminResponseModel>> CreateSubAdminAsync(CreateSubAdminRequestModel model);
        Task<bool> ApproveSubAdminAsync(string id);
        Task<bool> RejectSubAdminAsync(string id);
    }
}
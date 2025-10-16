using cbtBackend.Dtos.RequestModels;
using cbtBackend.Dtos.ResponseModels;

namespace cbtBackend.Services.Interfaces
{
    public interface ISubAdminService
    {
        Task<BaseResponse<CreateSubAdminResponseModel>> CreateSubAdminAsync(CreateSubAdminRequestModel model);
        Task<BaseResponse<CreateSubAdminResponseModel>> UpdateSubAdminAsync(CreateSubAdminRequestModel model);
        Task<BaseResponse<ICollection<SubAdminDto>>> GetAllSubAdminAsync();
        Task<BaseResponse<ICollection<SubAdminDto>>> GetAllUnApprovedAsync();
        Task<bool> ApproveSubAdminAsync(string id);
        Task<bool> RejectSubAdminAsync(string id);
    }
}
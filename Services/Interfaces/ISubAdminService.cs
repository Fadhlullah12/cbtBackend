using cbtBackend.Dtos.RequestModels;
using cbtBackend.Dtos.ResponseModels;

namespace cbtBackend.Services.Interfaces
{
    public interface ISubAdminService
    {
        Task<BaseResponse<CreateSubAdminResponseModel>> CreateSubAdminAsync(CreateSubAdminRequestModel model);
        Task<BaseResponse<ICollection<SubAdminDto>>> GetUnApprovedSubAdminsAsync();
        Task<BaseResponse<ICollection<SubAdminDto>>> GetAllSubAdminsAsync();
        Task<bool> ApproveSubAdminAsync(string id);
        Task<bool> RejectSubAdminAsync(string id);
    }
}
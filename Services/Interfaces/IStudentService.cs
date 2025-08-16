using cbtBackend.Dtos.RequestModels;
using cbtBackend.Dtos.ResponseModels;

namespace cbtBackend.Services.Interfaces
{
    public interface IStudentService
    {
       Task<BaseResponse<CreateStudentResponseModel>> LoginAsync(CreateStudentRequestModel model);   
    }
}
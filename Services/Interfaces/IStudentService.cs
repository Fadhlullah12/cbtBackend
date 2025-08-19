using cbtBackend.Dtos.RequestModels;
using cbtBackend.Dtos.ResponseModels;

namespace cbtBackend.Services.Interfaces
{
    public interface IStudentService
    {
        Task<BaseResponse<CreateStudentResponseModel>> RegisterStudent(CreateStudentRequestModel model);
        Task<BaseResponse<CreateMultipleStudentResponseModel>> RegisterStudents(IEnumerable<CreateStudentRequestModel> models);
    }
}
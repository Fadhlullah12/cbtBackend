using cbtBackend.Dtos.RequestModels;
using cbtBackend.Dtos.ResponseModels;

namespace cbtBackend.Services.Interfaces
{
    public interface IResultService
    {
        Task<BaseResponse<ICollection<ResultDto>>> GetStudentResultAsync(string studentId);
        Task<BaseResponse<ICollection<ResultDto>>> GetSubjectResultAsync(string subjectId);
        
    }
}
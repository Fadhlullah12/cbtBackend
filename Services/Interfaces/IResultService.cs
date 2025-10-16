using cbtBackend.Dtos.ResponseModels;

namespace cbtBackend.Services.Interfaces
{
    public interface IResultService
    {
        Task<BaseResponse<ICollection<StudentResultsDto>>> GetStudentResultAsync(string studentId);
        Task<BaseResponse<ICollection<StudentResultsDto>>> GetSubjectResultAsync(string subjectId);
        Task<BaseResponse<ICollection<ExamResultsDto>>> GetExamResultsAsync(string examId);
        
    }
}
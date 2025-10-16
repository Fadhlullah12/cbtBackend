using cbtBackend.Dtos.RequestModels;
using cbtBackend.Dtos.ResponseModels;

namespace cbtBackend.Services.Interfaces
{
    public interface IExamService
    {
        Task<BaseResponse<CreateExamResponseModel>> StartExamAsync(CreateExamRequestModel model);
        Task<BaseResponse<ICollection<ExamDto>>> GetAllExamsAsync();
        Task<BaseResponse<EndExamResponseModel>> EndExamAsync(string id);
        Task<BaseResponse<ICollection<LoadExamsDto>>> LoadAvailableExamAsync();
        Task<BaseResponse<SubmitExamDto>> SubmitExam(ExamSubmissionRequestModel model);
        Task<BaseResponse<ICollection<ExamDto>>> GetOngoingExamsAsync();

    }
}
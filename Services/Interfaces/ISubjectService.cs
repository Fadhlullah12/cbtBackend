using cbtBackend.Dtos.RequestModels;
using cbtBackend.Dtos.ResponseModels;

namespace cbtBackend.Services.Interfaces
{
    public interface ISubjectService
    {
        Task<BaseResponse<CreateSubjectResponseModel>> CreateSubjectAsync(CreateSubjectRequestModel model);
        Task<BaseResponse<ICollection<SubjectDto>>> ViewAllSubjectAsync();
        Task<bool> UploadSubjectQuestionsAsync(UploadQuestionRequestModel model);
        Task<BaseResponse<ICollection<StudentDto>>> ViewAllSubjectStudentAsync(string subjectId);
    }
}
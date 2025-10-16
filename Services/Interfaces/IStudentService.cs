using cbtBackend.Dtos.RequestModels;
using cbtBackend.Dtos.ResponseModels;

namespace cbtBackend.Services.Interfaces
{
    public interface IStudentService
    {
        Task<bool> Delete(string id);
        Task<bool> DeleteStudentSubject(string id);
        Task<bool> AssignSubjects(AssignSubjectsRequestModel model);
        Task<BaseResponse<ICollection<StudentDto>>> GetAllStudentsAsync();
        Task<BaseResponse<StudentDto>> UpdateStudent(UpdateStudentRequestModel model);
        Task<BaseResponse<ICollection<ResultsDto>>> ViewStudentResultAsync(string studentId);
        Task<BaseResponse<ICollection<ExamDto>>> ViewAllStudentExamsAsync(string studentId);
        Task<BaseResponse<ICollection<AllStudentsResultsdto>>> ViewStudentsResultAsync();
        Task<BaseResponse<ICollection<SubjectDto>>> ViewAllStudentSubjectAsync(string studentId);
        Task<BaseResponse<CreateStudentResponseModel>> RegisterStudent(CreateStudentRequestModel model);
        Task<BaseResponse<CreateMultipleStudentResponseModel>> RegisterStudents(IEnumerable<CreateStudentRequestModel> models);
    }
}
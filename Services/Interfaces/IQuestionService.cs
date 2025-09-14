using cbtBackend.Dtos.RequestModels.UpdateRequstModels;
using cbtBackend.Dtos.ResponseModels;

namespace cbtBackend.Services.Interfaces
{
    public interface IQuestionService
    {
        public Task<BaseResponse<ExamResponse>> LoadExamQuestions(string examId);
        Task<BaseResponse<ICollection<QuestionDto>>> GetSubjectQuestions(string subjectId);
        Task<bool> Delete(string questionId);
        Task<bool> UpdateQuestion(UpdateQuestionsRequestModel model);
    }
}
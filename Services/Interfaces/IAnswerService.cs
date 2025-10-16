using cbtBackend.Dtos.RequestModels.UpdateRequstModels;
using cbtBackend.Dtos.ResponseModels;

namespace cbtBackend.Services.Interfaces
{
    public interface IAnswerService
    {
        public Task<bool> Delete(string answerId);
        public Task<BaseResponse<ICollection<AnswerDto>>> GetQuestionAnswers(string questionId);
        public Task<bool> Update(UpdateAnswerRequestModel model);

    }
}
using cbtBackend.Dtos.RequestModels.UpdateRequstModels;

namespace cbtBackend.Services.Interfaces
{
    public interface IAnswerService
    {
        public Task<bool> Delete(string answerId);
        public Task<bool> Update(UpdateAnswerRequestModel model);

    }
}
using cbtBackend.Dtos.ResponseModels;

namespace cbtBackend.Services.MailService
{
    public interface IMailMessageService
    {
        Task<bool> SendResult(SendResultDto model);
        Task<bool> SendPlainMessage(MessageDto model);
        Task<bool> SendAprovalMessage(string userName, string email, bool isApproved);
    }
}
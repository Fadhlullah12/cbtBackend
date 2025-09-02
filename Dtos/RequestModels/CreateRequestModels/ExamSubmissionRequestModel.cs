using cbtBackend.Dtos.ResponseModels;

namespace cbtBackend.Dtos.RequestModels
{
    public class ExamSubmissionRequestModel
    {
        public string ExamId { get; set; } = default!;
        public List<AnswerItem> Answers { get; set; } = [];
    }
}
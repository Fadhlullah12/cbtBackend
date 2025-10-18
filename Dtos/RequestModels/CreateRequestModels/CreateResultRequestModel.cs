namespace cbtBackend.Dtos.RequestModels
{
    public class CreateResultRequestModel
    {
        public int Score { get; set; } = default!;
        public string ExamId { get; set; } = default!;
        public string StudentId { get; set; } = default!;
        public string SubjectId { get; set; } = default!;
         
    }
}
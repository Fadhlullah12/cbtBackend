namespace cbtBackend.Dtos.ResponseModels
{
    public class CreateResultResponseModel
    {
        public int Score { get; set; } = default!;
        public string ExamId { get; set; } = default!;
        public string StudentName { get; set; } = default!;
        public string SubjectName { get; set; } = default!; 
    }
}
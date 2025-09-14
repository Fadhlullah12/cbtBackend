namespace cbtBackend.Dtos.ResponseModels
{
    public class ResultDto
    {
        public int Score { get; set; } = default!;
        public string StudentName { get; set; } = default!;
        public string SubjectName { get; set; } = default!;
        public DateTime DateCreated { get; set; } = default!;
    }
}
namespace cbtBackend.Dtos.ResponseModels
{
        public class SubmittedResultDto
    {
        public string Subject { get; set; } = default!;
        public string Title { get; set; } = default!;
        public int Score { get; set; } = default!;
        public int Questions { get; set; } = default!;
        public int Percentage { get; set; } = default!;
    }

}
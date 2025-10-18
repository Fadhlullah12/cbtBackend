namespace cbtBackend.Dtos.ResponseModels
{
    public class ProfileResults
    {
        public string Title { get; set; } = default!;
        public int Score { get; set; } = default!;
        public string Date { get; set; } = default!;
        public int Questions { get; set; } = default!;
    }
}
namespace cbtBackend.Dtos.RequestModels
{
    public class CreateExamRequestModel
    {
        public string Title { get; set; } = default!;
        public int MaxQuestion { get; set; } = default!;
        public int DurationMinutes { get; set; } = default!;
        public string SubjectName { get; set; } = default!;
        public DateTime TimeScheduled { get; set; } = default!;
    }
}
namespace cbtBackend.Dtos.ResponseModels
{
    public class ExamResponse
    {
        public string SubjectName { get; set; } = default!;
        public int DurationMinutes { get; set; }
        public ICollection<ExamQuestionsDto> Questions { get; set; } = [];
    }
}
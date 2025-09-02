namespace cbtBackend.Dtos.ResponseModels
{
    public class ExamQuestionsDto
    {
        public string QuestionId { get; set; } = default!;
        public string Question { get; set; } = default!;
        public ICollection<string> Options { get; set; } = [];
    }
}
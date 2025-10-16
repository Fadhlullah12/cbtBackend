namespace cbtBackend.Dtos.ResponseModels
{
    public class QuestionDto
    {
        public string Label { get; set; } = default!;
        public ICollection<string> Answers { get; set; } = [];

    }
}
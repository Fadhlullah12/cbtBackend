namespace cbtBackend.Dtos.ResponseModels
{
    public class AnswerDto
    {
        public string Label { get; set; } = default!;
        public string Id { get; set; } = default!;
        public bool IsCorrect { get; set; } = default!;
    }
}
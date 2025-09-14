namespace cbtBackend.Dtos.ResponseModels
{
    public class AnswerItem
    {
        public string QuestionId { get; set; } = default!;
        public string SelectedOption { get; set; } = default!;
    }
}
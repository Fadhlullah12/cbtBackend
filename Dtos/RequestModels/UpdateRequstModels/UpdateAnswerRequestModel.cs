namespace cbtBackend.Dtos.RequestModels.UpdateRequstModels
{
    public class UpdateAnswerRequestModel
    {
        public string AnswerId { get; set; } = default!;
        public string Label { get; set; } = default!;
        public bool IsCorrect{ get; set; }
    }
}
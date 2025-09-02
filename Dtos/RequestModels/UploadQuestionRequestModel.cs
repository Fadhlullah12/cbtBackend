namespace cbtBackend.Dtos.RequestModels
{
    public class UploadQuestionRequestModel
    {
        public string Id { get; set; } = default!;
        public IFormFile QuestionFile { get; set; } = default!;
    }
}
namespace cbtBackend.Dtos.RequestModels
{
    public class CreateSubjectRequestModel
    {
        public string SubjectName { get; set; } = default!;
        public IFormFile? QuestionFile { get; set; } 
    }
}
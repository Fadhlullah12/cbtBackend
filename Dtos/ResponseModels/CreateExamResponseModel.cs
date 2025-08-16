namespace cbtBackend.Dtos.ResponseModels
{
    public class CreateExamResponseModel
    {
        public string QuestionFilePath { get; set; } = default!;
        public string SubjectName { get; set; } = default!;
    }
}
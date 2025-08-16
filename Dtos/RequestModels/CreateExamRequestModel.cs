namespace cbtBackend.Dtos.RequestModels
{
    public class CreateExamRequestModel
    {
        public string QuestionFilePath { get; set; } = default!;
        public string SubjectName { get; set; } = default!;
        public string SubAdminId { get; set; } = default!;
    }
}
namespace cbtBackend.Dtos.ResponseModels
{
    public class LoadExamsDto
    {
        public string ExamId { get; set; } = default!;
        public string SubjectName { get; set; } = default!;
        public int NoOfQuestions { get; set; } = default!;
        public string Title { get; set; } = default!;
    }
}
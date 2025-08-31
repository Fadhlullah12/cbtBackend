namespace cbtBackend.Dtos.ResponseModels
{
    public class SubjectDto
    {
        public string SubjectName { get; set; } = default!;
        public int SubjectExams { get; set; }
        public int SubjectStudents { get; set; }

    }
}
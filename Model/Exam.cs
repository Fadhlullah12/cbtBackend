namespace cbtBackend.Model
{
    public class Exam : BaseEntity
    {
        public string QuestionFilePath { get; set; } = default!;
        public SubAdmin SubAdmin { get; set; } = default!;
        public string SubAdminId { get; set; } = default!;
        public Subject Subject { get; set; } = default!;
        public string SubjectId { get; set; } = default!;
    }
}
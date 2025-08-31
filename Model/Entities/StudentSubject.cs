namespace cbtBackend.Model
{
    public class StudentSubject : BaseEntity
    {
        public Student Student { get; set; } = default!;
        public string StudentId { get; set; } = default!;
        public Subject Subject { get; set; } = default!;
        public string SubjectId { get; set; } = default!;
    }
}
namespace cbtBackend.Model
{
    public class StudentSubject
    {
        public Student Student { get; set; } = default!;
        public string StudentId { get; set; } = default!;
        public Subject Subject { get; set; } = default!;
        public string SubjectId { get; set; } = default!;
    }
}
namespace cbtBackend.Model
{
    public class Result : BaseEntity
    {
        public int Score { get; set; } = default!;
        public Exam Exam { get; set; } = default!;
        public string ExamId { get; set; } = default!;
        public Student Student { get; set; } = default!;
        public string StudentId { get; set; } = default!;
        public Subject Subject { get; set; } = default!;
        public string SubjectId { get; set; } = default!;
         
    }
}
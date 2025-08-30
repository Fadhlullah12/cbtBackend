namespace cbtBackend.Model.Entities
{
    public class StudentExam : BaseEntity
    {
        public Student Student { get; set; } = default!;
        public string StudentId { get; set; } = default!;
        public Exam Exam { get; set; } = default!;
        public string ExamId { get; set; } = default!;
    }
}
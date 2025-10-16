namespace cbtBackend.Model
{
    public class SubAdmin : BaseEntity
    {
        public User User { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public ICollection<Student> Students = [];
        public ICollection<Exam> Exams = [];
    }
}
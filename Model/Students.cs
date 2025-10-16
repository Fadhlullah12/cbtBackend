namespace cbtBackend.Model
{
    public class Student : BaseEntity
    {
        public User User { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public string SerialNumber { get; set; } = default!;
        public SubAdmin SubAdmin { get; set; } = default!;
        public string SubAdminId { get; set; } = default!;
        public ICollection<StudentSubject> StudentSubjects = [];
        public ICollection<Result> Results = [];
    }
}
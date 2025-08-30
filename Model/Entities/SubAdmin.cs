using cbtBackend.Model.Enums;

namespace cbtBackend.Model
{
    public class SubAdmin : BaseEntity
    {
        public User User { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public ApprovalStatus ApprovalStatus { get; set; } = ApprovalStatus.Pending;
        public ICollection<Student> Students = [];
        public ICollection<Subject> Subjects = [];
        public ICollection<Exam> Exams = [];
    }
}
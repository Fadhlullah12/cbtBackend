using cbtBackend.Model.Entities;

namespace cbtBackend.Model
{
    public class Exam : BaseEntity
    {
        public string Title { get; set; } = default!;
        public int MaxQuestion { get; set; } = default!;
        public int DurationMinutes { get; set; } = default!;
        public bool Ongoing { get; set; } = true;
        public DateTime TimeScheduled { get; set; } = default!;
        public SubAdmin SubAdmin { get; set; } = default!;
        public string SubAdminId { get; set; } = default!;
        public Subject Subject { get; set; } = default!;
        public string SubjectId { get; set; } = default!;
        public ICollection<Result> Results = [];
        public ICollection<StudentExam> StudentExams = [];
    }
    
}
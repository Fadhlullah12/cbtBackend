using cbtBackend.Model.Entities;

namespace cbtBackend.Model
{
    public class Subject : BaseEntity
    {
        public string SubjectName { get; set; } = default!;
        public SubAdmin SubAdmin { get; set; } = default!;
        public string SubAdminId { get; set; } = default!;
        public ICollection<StudentSubject> StudentSubjects { get; set; } = [];
        public ICollection<Result> Results { get; set; } = [];
        public ICollection<Question> Questions { get; set; } = [];
        public ICollection<Exam> Exams { get; set; } = [];
    }
    
    
}
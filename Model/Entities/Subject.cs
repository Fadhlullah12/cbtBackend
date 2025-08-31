using cbtBackend.Model.Entities;

namespace cbtBackend.Model
{
    public class Subject : BaseEntity
    {
        public string SubjectName { get; set; } = default!;
        public SubAdmin SubAdmin { get; set; } = default!;
        public string SubAdminId { get; set; } = default!;
        public ICollection<StudentSubject> StudentSubjects = [];
        public ICollection<Result> Results = [];
        public ICollection<Question> Questions = [];
        public ICollection<Exam> Exams = [];
    }
    
    
}
namespace cbtBackend.Model
{
    public class Subject : BaseEntity
    {
        public string SubjectName { get; set; } = default!;
        public ICollection<StudentSubject> StudentSubjects = [];
        public ICollection<Exam> Exams = [];
    }
    
    
}
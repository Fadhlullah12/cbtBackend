namespace cbtBackend.Model
{
    public class Subject : BaseEntity
    {
        public string SubjectName { get; set; } = default!;
        public SubAdmin SubAdmin { get; set; } = default!;
        public string SubAdminId { get; set; } = default!;
        public ICollection<Student> Students = [];
        public ICollection<Subject> Subjects = [];
    }
    
}
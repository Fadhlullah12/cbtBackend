namespace cbtBackend.Model
{
    public class Student : BaseEntity
    {
        public string SerialNumber { get; set; } = default!;
        public SubAdmin SubAdmin { get; set; } = default!;
        public string SubAdminId { get; set; } = default!;
        public ICollection<Subject> Subjects = [];
        public ICollection<Result> Results = [];
    }
}
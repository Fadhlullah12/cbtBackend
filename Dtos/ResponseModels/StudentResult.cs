namespace cbtBackend.Dtos.ResponseModels
{
    public class StudentResult
    {
        public string SubjectName { get; set; } = default!;
        public string Id { get; set; } = default!;
        public ICollection<ProfileResults> ProfileResults { get; set; } = default!;
    }
}
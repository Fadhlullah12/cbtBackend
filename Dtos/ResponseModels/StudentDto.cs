namespace cbtBackend.Dtos.ResponseModels
{
    public class StudentDto
    {
        public string FullName { get; set; } = default!;
        public string Id { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string SerialNumber { get; set; } = default!;
        public int Subjects { get; set; } =  default!;
    }
}
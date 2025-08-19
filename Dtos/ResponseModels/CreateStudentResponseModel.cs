namespace cbtBackend.Dtos.ResponseModels
{
    public class CreateStudentResponseModel
    {
        public string Email { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string SerialNumber { get; set; } = default!;
    }
}
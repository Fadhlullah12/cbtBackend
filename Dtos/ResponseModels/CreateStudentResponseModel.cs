namespace cbtBackend.Dtos.ResponseModels
{
    public class CreateStudentResponseModel
    {
        public string Email { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string SerialNumber { get; set; } = default!;
    }
}
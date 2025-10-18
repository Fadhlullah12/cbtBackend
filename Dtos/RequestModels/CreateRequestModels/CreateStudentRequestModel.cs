namespace cbtBackend.Dtos.RequestModels
{
    public class CreateStudentRequestModel
    {
        public string Email { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
    }
}
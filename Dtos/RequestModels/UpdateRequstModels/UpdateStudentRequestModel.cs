namespace cbtBackend.Dtos.RequestModels
{
    public class UpdateStudentRequestModel
    {
        public string Id { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
    }
}
namespace cbtBackend.Dtos.RequestModels
{
    public class CreateSubAdminRequestModel
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
    }
}
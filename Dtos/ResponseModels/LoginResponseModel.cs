namespace cbtBackend.Dtos.ResponseModels
{
    public class LoginResponseModel
    {
        public string Id { get; set; } = default!;
        public string Role { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;

    }
}
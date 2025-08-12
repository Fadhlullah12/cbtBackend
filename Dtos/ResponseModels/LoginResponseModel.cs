namespace cbtBackend.Dtos.ResponseModels
{
    public class LoginResponseModel
    {
        public string Role { get; set; } = default!;
        public string UserName { get; set;} = default!;
    }
}
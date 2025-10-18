namespace cbtBackend.Dtos.ResponseModels
{
    public class MessageDto
    {
        public string UserName { get; set; } = default!;
        public string Message { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
namespace cbtBackend.Dtos.ResponseModels
{
        public class SendResultDto
    {
        public string UserName { get; set; } = default!;
        public string UserEmail { get; set; } = default!;
        public List<SubmittedResultDto> Results { get; set; } = default!;
    }
}
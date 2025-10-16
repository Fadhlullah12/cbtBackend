namespace cbtBackend.Dtos.ResponseModels
{
    public class SubmitExamDto
    {
        public int score { get; set; }
        public Dictionary<string, string> Review { get; set; } = [];
    }
}
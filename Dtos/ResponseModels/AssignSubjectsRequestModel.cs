namespace cbtBackend.Dtos.ResponseModels
{
    public class AssignSubjectsRequestModel
    {
        public string StudentId { get; set; } = default!;
        public ICollection<string> SubjectIds { get; set; } = default!;

    }
}
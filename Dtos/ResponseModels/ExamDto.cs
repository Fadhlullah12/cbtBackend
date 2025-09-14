namespace cbtBackend.Dtos.ResponseModels
{
    public class ExamDto
    {
        public string Title { get; set; } = default!;
        public string Id { get; set; } = default!;
        public bool Ongoing { get; set; } = default!;
        public string SubjectName { get; set; } = default!;
        public ICollection<StudentDto> Students = [];
        public DateTime DateCreated { get; set; } = default!;

    }
}

namespace cbtBackend.Dtos.ResponseModels
{
    public class HomePageDto
    {
        public int NoOfSubjects { get; set; } = default!;
        public int NoOfStudents { get; set; } = default!;
        public int NoOfExamsAdministered { get; set; } = default!;
        public Dictionary<string, int> SubjectStudents { get; set; } = [];
    }
}
namespace cbtBackend.Dtos.ResponseModels
{
    public class AllStudentsResultsdto
    {
        public string FullName { get; set; } = default!;
        public List<MultipleStudentResultDto> Results { get; set; } = [];
    }
}
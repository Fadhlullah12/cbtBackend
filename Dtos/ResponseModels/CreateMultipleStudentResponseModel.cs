namespace cbtBackend.Dtos.ResponseModels
{
    public class CreateMultipleStudentResponseModel
    {
        public ICollection<CreateStudentResponseModel> registeredStudents { get; set; } = [];
        public ICollection<CreateStudentResponseModel> unRegisteredStudents { get; set; } = [];
    }
}

namespace cbtBackend.Dtos.ResponseModels
{
    public class CreateMultipleStudentResponseModel
    {
        public ICollection<CreateStudentResponseModel> registeredStudents = [];
        public ICollection<CreateStudentResponseModel> unRegisteredStudents = [];
    }
}

using cbtBackend.Model;

namespace cbtBackend.Dtos.ResponseModels
{
    public class EndExamResponseModel
    {
       public ICollection<StudentDto> Students { get; set; } = [];
    }
}
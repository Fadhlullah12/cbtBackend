using cbtbackend.Repositories.Interface;
using cbtBackend.Model.Entities;

namespace cbtBackend.Repositories.Interfaces
{
    public interface IStudentExamRepository : IBaseRepository<StudentExam>
    {
         Task<ICollection<StudentExam>> GetStudentsAsync(string Id);
    }
}
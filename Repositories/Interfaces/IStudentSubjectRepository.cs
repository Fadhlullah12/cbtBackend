using cbtbackend.Repositories.Interface;
using cbtBackend.Model;

namespace cbtBackend.Repositories.Interfaces
{
    public interface IStudentSubjectRepository : IBaseRepository<StudentSubject>
    {
        Task<ICollection<StudentSubject>> GetStudentsAsync(string Id);
        Task<ICollection<StudentSubject>> GetSubjectsAsync(string Id);
        Task<StudentSubject> GetSubject(string Id);
    }
}
using System.Linq.Expressions;
using cbtBackend.Model;

namespace cbtBackend.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        Task<Student> Get(string id);
        Task<Student> Get(Expression<Func<Student, bool>> expression);
        Task<ICollection<Student>> GetAll();   
    }
}
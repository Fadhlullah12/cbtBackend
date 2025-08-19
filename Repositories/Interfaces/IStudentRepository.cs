using System.Linq.Expressions;
using cbtbackend.Repositories.Interface;
using cbtBackend.Model;

namespace cbtBackend.Repositories.Interfaces
{
    public interface IStudentRepository : IBaseRepository<Student>
    {
        Task<Student> Get(string id);
        Task<Student> Get(Expression<Func<Student, bool>> expression);
        Task<ICollection<Student>> GetAll();   
    }
}
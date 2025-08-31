using System.Linq.Expressions;
using cbtbackend.Repositories.Interface;
using cbtBackend.Model;

namespace cbtBackend.Repositories.Interfaces
{
    public interface ISubjectRepository : IBaseRepository<Subject>
    {
        Task<Subject> Get(string id);
        Task<Subject> Get(Expression<Func<Subject, bool>> expression);
        Task<ICollection<Subject>> GetAll(Expression<Func<Subject, bool>> predicate);
    }
}
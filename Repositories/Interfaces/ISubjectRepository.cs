using System.Linq.Expressions;
using cbtBackend.Model;

namespace cbtBackend.Repositories.Interfaces
{
    public interface ISubjectRepository
    {
        Task<Subject> Get(string id);
        Task<Subject> Get(Expression<Func<Subject, bool>> expression);
        Task<ICollection<Subject>> GetAll(); 
    }
}
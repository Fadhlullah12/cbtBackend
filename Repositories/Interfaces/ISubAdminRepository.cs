using System.Linq.Expressions;
using cbtBackend.Model;

namespace cbtBackend.Repositories.Interfaces
{
    public interface ISubAdminRepository
    {
        Task<SubAdmin> Get(string id);
        Task<SubAdmin> Get(Expression<Func<SubAdmin, bool>> expression);
        Task<ICollection<SubAdmin>> GetAll(); 
    }
}
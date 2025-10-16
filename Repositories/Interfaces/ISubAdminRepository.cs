using System.Linq.Expressions;
using cbtbackend.Repositories.Interface;
using cbtBackend.Model;

namespace cbtBackend.Repositories.Interfaces
{
    public interface ISubAdminRepository : IBaseRepository<SubAdmin>
    {
        Task<SubAdmin> Get(string id);
        Task<ICollection<SubAdmin>> GetUnApproved();
        Task<SubAdmin> Get(Expression<Func<SubAdmin, bool>> expression);
        Task<ICollection<SubAdmin>> GetAll(Expression<Func<SubAdmin, bool>> expression);
        Task<ICollection<SubAdmin>> GetAll(); 
    }
}
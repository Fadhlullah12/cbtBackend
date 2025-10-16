using cbtbackend.Repositories.Interface;
using cbtBackend.Model;
using System.Linq.Expressions;
namespace cbtBackend.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> Get(string id);
        Task<User> Get(Expression<Func<User, bool>> predicate);
    }
}
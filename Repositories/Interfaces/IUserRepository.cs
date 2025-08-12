using cbtBackend.Model;
using System.Linq.Expressions;
namespace cbtBackend.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task <User> Get(Expression<Func<User, bool>> predicate);
    }
}
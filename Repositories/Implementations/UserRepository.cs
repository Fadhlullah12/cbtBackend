using System.Linq.Expressions;
using cbtBackend.Context;
using cbtBackend.Model;
using cbtBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace cbtBackend.Repositories.Implementations
{
    public class UserRepository : BaseRepository<User> ,IUserRepository
    {
        ApplicationContext _applicationContext;
        public UserRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<User> Get(Expression<Func<User, bool>> predicate)
        {
             var user = await _applicationContext.Set<User>()
             .FirstOrDefaultAsync(predicate);
            return user!;
        }
    }
}
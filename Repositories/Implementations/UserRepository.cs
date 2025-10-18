using System.Linq.Expressions;
using cbtBackend.Context;
using cbtBackend.Model;
using cbtBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace cbtBackend.Repositories.Implementations
{
    public class UserRepository : BaseRepository<User> ,IUserRepository
    {
        public UserRepository(ApplicationContext applicationContext)
        {
            _context = applicationContext;
        }

        public async Task<User> Get(Expression<Func<User, bool>> predicate)
        {
             var user = await _context.Set<User>()
             .Include(a => a.SubAdmin)
             .Include(a => a.Student)
             .FirstOrDefaultAsync(predicate);
            return user!;
        }
    }
}
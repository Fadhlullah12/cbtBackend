using System.Linq.Expressions;
using cbtBackend.Context;
using cbtBackend.Model;
using cbtBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace cbtBackend.Repositories.Implementations
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<User> Get(Expression<Func<User, bool>> predicate)
        {
             var user = await _context.Set<User>()
            .Include(a => a.Student)
            .Include(a => a.SubAdmin)
            .FirstOrDefaultAsync(predicate);
            return user!;
        }
      
        public async Task<User> Get(string id)
        {
            var user = await _context.Set<User>()
            .Include(a => a.Student)
            .Include(a => a.SubAdmin)
            .FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false);
            return user!;
        }
        
    }
}
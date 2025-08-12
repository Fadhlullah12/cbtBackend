using System.Linq.Expressions;
using cbtBackend.Context;
using cbtBackend.Model;
using cbtBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace cbtBackend.Repositories.Implementations
{
    public class SubAdminRepository : BaseRepository<SubAdmin>, ISubAdminRepository
    {
        ApplicationContext _context;
        public SubAdminRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<SubAdmin> Get(string id)
        {
            var subAdmin = await _context.Set<SubAdmin>()
            .Include(a => a.Subjects)
            .Include(a => a.Students)
            .Include(a => a.User)
            .FirstOrDefaultAsync(a => a.Id == id);
            return subAdmin!;
        }

        public async Task<SubAdmin> Get(Expression<Func<SubAdmin, bool>> expression)
        {
             var subAdmin = await _context.Set<SubAdmin>()
            .Include(a => a.Subjects)
            .Include(a => a.Students)
            .Include(a => a.User)
            .FirstOrDefaultAsync(expression);
            return subAdmin!;
        }

        public async Task<ICollection<SubAdmin>> GetAll()
        {
             var subAdmin = await _context.Set<SubAdmin>()
            .Include(a => a.Subjects)
            .Include(a => a.Students)
            .Include(a => a.User)
            .ToListAsync();
            return subAdmin!;
        }
    }
}
using System.Linq.Expressions;
using cbtBackend.Context;
using cbtBackend.Model;
using cbtBackend.Model.Enums;
using cbtBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace cbtBackend.Repositories.Implementations
{
    public class SubAdminRepository : BaseRepository<SubAdmin>, ISubAdminRepository
    {
        public SubAdminRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<SubAdmin> Get(string id)
        {
            var subAdmin = await _context.Set<SubAdmin>()
            .Include(a => a.Students)
            .Include(a => a.User)
            .FirstOrDefaultAsync(a => a.Id == id);
            return subAdmin!;
        }
        public async Task<ICollection<SubAdmin>> GetUnApproved()
        {
            var subAdmin = await _context.Set<SubAdmin>()
            .Include(a => a.Students)
            .Include(a => a.User)
            .Where(a => a.ApprovalStatus == ApprovalStatus.Pending)
            .ToListAsync();
            return subAdmin!;
        }

        public async Task<SubAdmin> Get(Expression<Func<SubAdmin, bool>> expression)
        {
             var subAdmin = await _context.Set<SubAdmin>()
            .Include(a => a.User)
            .Include(a => a.Subjects)
            .ThenInclude(a => a.Results)
            .Include(a => a.Exams)
            .ThenInclude(a => a.Subject)
            .Include(a => a.Students)
            .ThenInclude(a => a.User)
            .Include(a => a.Students)
            .ThenInclude(a => a.StudentSubjects)
            .FirstOrDefaultAsync(expression);
            return subAdmin!;
        }

        public async Task<ICollection<SubAdmin>> GetAll()
        {
             var subAdmin = await _context.Set<SubAdmin>()
            .Include(a => a.Students)
            .Include(a => a.User)
            .Where(a => a.ApprovalStatus == ApprovalStatus.Approved)
            .ToListAsync();
            return subAdmin!;
        }
    }
}
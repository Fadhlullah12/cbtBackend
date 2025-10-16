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
            .Include(a => a.User)
            .Include(a => a.Subjects)
            .Include(a => a.Students)
            .Include(a => a.Exams)
            .FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false);
            return subAdmin!;
        }
        public async Task<ICollection<SubAdmin>> GetUnApproved()
        {
            var subAdmin = await _context.Set<SubAdmin>()
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
            .ThenInclude(a => a.Questions)
            .Include(a => a.Students)
            .ThenInclude(a => a.User)
            .Include(a => a.Students)
            .ThenInclude(a => a.StudentExams)
             .Include(a => a.Students)
            .ThenInclude(a => a.StudentSubjects)
            .Include(a => a.Exams)
            .AsSplitQuery()
           .FirstOrDefaultAsync(expression);
            return subAdmin!;
        }

        public async Task<ICollection<SubAdmin>> GetAll()
        {
             var subAdmin = await _context.Set<SubAdmin>()
            .Include(a => a.User)
            .Include(a => a.Subjects)
            .Include(a => a.Students)
            .Include(a => a.Exams)
            .Where(a => a.IsDeleted == false)
            .ToListAsync();
            return subAdmin!;
        }

        public async Task<ICollection<SubAdmin>> GetAll(Expression<Func<SubAdmin, bool>> expression)
        {
             var exam = await _context.Set<SubAdmin>()
            .Include(a => a.User)
            .Include(a => a.Subjects)
            .Include(a => a.Students)
            .Include(a => a.Exams)
           .Where(expression)
           .ToListAsync();
            return exam!;
        }
    }
}
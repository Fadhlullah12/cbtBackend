using System.Linq.Expressions;
using cbtBackend.Context;
using cbtBackend.Model;
using cbtBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace cbtBackend.Repositories.Implementations
{
    public class ExamRepository : BaseRepository<Exam>, IExamRepository
    {
        ApplicationContext _context;
        public ExamRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Exam> Get(string id)
        {
            var exam = await _context.Set<Exam>()
           .Include(a => a.Subject)
           .Include(a => a.SubAdmin)
           .FirstOrDefaultAsync(a => a.Id == id);
            return exam!;
        }

        public async Task<Exam> Get(Expression<Func<Exam, bool>> expression)
        {
            var exam = await _context.Set<Exam>()
           .Include(a => a.Subject)
           .Include(a => a.SubAdmin)
           .FirstOrDefaultAsync(expression);
            return exam!; 
        }

        public async Task<ICollection<Exam>> GetAll()
        {
             var exam = await _context.Set<Exam>()
           .Include(a => a.Subject)
           .Include(a => a.SubAdmin)
           .ToListAsync();
            return exam!;
        }
    }
}
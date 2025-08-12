using System.Linq.Expressions;
using cbtBackend.Context;
using cbtBackend.Model;
using cbtBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace cbtBackend.Repositories.Implementations
{
    public class ResultRepository : BaseRepository<Result>, IResultRepository
    {
        ApplicationContext _context;
        public ResultRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Result> Get(string id)
        {
            var result = await _context.Set<Result>()
           .Include(a => a.Student)
           .Include(a => a.Exam)
           .FirstOrDefaultAsync(a => a.Id == id);
            return result!;
        }

        public async Task<Result> Get(Expression<Func<Result, bool>> expression)
        {
            var result = await _context.Set<Result>()
           .Include(a => a.Student)
           .Include(a => a.Exam)
           .FirstOrDefaultAsync(expression);
            return result!;
        }

        public async Task<ICollection<Result>> GetAll()
        {
            var result = await _context.Set<Result>()
           .Include(a => a.Student)
           .Include(a => a.Exam)
           .ToListAsync();
            return result!;
        }
    }
}
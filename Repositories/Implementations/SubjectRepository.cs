using System.Linq.Expressions;
using cbtBackend.Context;
using cbtBackend.Model;
using cbtBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace cbtBackend.Repositories.Implementations
{
    public class SubjectRepository : BaseRepository<Subject>, ISubjectRepository
    {
        ApplicationContext _context;
        public SubjectRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Subject> Get(string id)
        {
            var subject = await _context.Set<Subject>()
            .Include(a => a.StudentSubjects)
            .FirstOrDefaultAsync(a => a.Id == id);
            return subject!;
        }

        public async Task<Subject> Get(Expression<Func<Subject, bool>> expression)
        {
             var subject = await _context.Set<Subject>()
            .Include(a => a.StudentSubjects)
            .FirstOrDefaultAsync(expression);
            return subject!;
        }

        public async Task<ICollection<Subject>> GetAll()
        {
            var subject = await _context.Set<Subject>()
            .Include(a => a.StudentSubjects)
            .ToListAsync();
            return subject!;
        }
    }
}
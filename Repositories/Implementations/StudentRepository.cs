using System.Linq.Expressions;
using cbtBackend.Context;
using cbtBackend.Model;
using cbtBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace cbtBackend.Repositories.Implementations
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        ApplicationContext _context;

        public StudentRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Student> Get(string id)
        {
            var student = await _context.Set<Student>()
            .Include(a => a.SubAdmin)
            .FirstOrDefaultAsync(a => a.Id == id);
            return student!;
        }

        public async Task<Student> Get(Expression<Func<Student, bool>> expression)
        {
             var student = await _context.Set<Student>()
            .Include(a => a.SubAdmin)
            .FirstOrDefaultAsync(expression);
            return student!;
        }

        public async Task<ICollection<Student>> GetAll()
        {
            var student = await _context.Set<Student>()
            .Include(a => a.SubAdmin)
            .ToListAsync();
            return student!;
        }
    }
}
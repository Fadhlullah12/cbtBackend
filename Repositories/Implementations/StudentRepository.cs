using System.Linq.Expressions;
using cbtBackend.Context;
using cbtBackend.Model;
using cbtBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace cbtBackend.Repositories.Implementations
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Student> Get(string id)
        {
         var student = await _context.Set<Student>()
        .Include(a => a.SubAdmin)
        .Include(a => a.User)
        .Include(a => a.Results)
        .Include(a => a.StudentExams)
        .Include(a => a.StudentSubjects)
            .ThenInclude(a => a.Subject)
                .ThenInclude(a => a.Exams)
        .AsSplitQuery()
        .FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false);

        return student!;

        }

        public async Task<Student> Get(Expression<Func<Student, bool>> expression)
        {
             var student = await _context.Set<Student>()
             .Include(a => a.SubAdmin)
            .Include(a => a.User)
            .Include(a => a.Results)
            .Include(a => a.StudentExams)
            .Include(a => a.StudentSubjects)
            .ThenInclude(a => a.Subject)
            .ThenInclude(a => a.Exams)
            .AsSplitQuery()
            .FirstOrDefaultAsync(expression);
            return student!;
        }

       public async Task<ICollection<Student>> GetAll(string subAdminId)
        {
            return await _context.Students
            .Include(a => a.SubAdmin)
            .Include(a => a.User)
            .Include(a => a.Results)
            .Include(a => a.StudentExams)
            .Include(a => a.StudentSubjects)
            .Where(a => !a.IsDeleted && a.SubAdminId == subAdminId)
            .AsSplitQuery()
            .ToListAsync();
        }


        public async Task<ICollection<Student>> GetAll(Expression<Func<Student, bool>> expression)
        {
              var student = await _context.Set<Student>()
             .Include(a => a.SubAdmin)
            .Include(a => a.User)
            .Include(a => a.Results)
            .Include(a => a.StudentExams)
            .Include(a => a.StudentSubjects)
            .ThenInclude(a => a.Subject)
            .ThenInclude(a => a.Exams)
            .AsSplitQuery()
            .Where(expression)
            .ToListAsync();
            return student!;
        }
    }
}
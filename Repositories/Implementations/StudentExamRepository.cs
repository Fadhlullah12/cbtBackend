using cbtBackend.Context;
using cbtBackend.Model.Entities;
using cbtBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace cbtBackend.Repositories.Implementations
{
    public class StudentExamRepository : BaseRepository<StudentExam>, IStudentExamRepository
    {
        public StudentExamRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ICollection<StudentExam>> GetStudentsAsync(string Id)
        {
            var exam = await _context.Set<StudentExam>()
           .Include(a => a.Student)
           .Include(a => a.Exam)
           .Where(a => a.StudentId == Id && a.IsDeleted == false)
           .ToListAsync();
            return exam!;
        }
    }
}
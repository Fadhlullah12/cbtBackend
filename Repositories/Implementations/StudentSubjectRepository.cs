using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cbtBackend.Context;
using cbtBackend.Model;
using cbtBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace cbtBackend.Repositories.Implementations
{
    public class StudentSubjectRepository : BaseRepository<StudentSubject>, IStudentSubjectRepository
    {
          public StudentSubjectRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ICollection<StudentSubject>> GetSubjectsAsync(string Id)
        {
             var students = await _context.Set<StudentSubject>()
           .Include(a => a.Student)
           .Include(a => a.Subject)
           .Where(a => a.StudentId == Id && a.IsDeleted == false)
           .ToListAsync();
            return students!;
        }

        public async Task<ICollection<StudentSubject>> GetStudentsAsync(string Id)
        {
              var subjects = await _context.Set<StudentSubject>()
           .Include(a => a.Student)
           .ThenInclude(a => a.User)
           .Include(a => a.Subject)
           .Where(a => a.SubjectId == Id && a.IsDeleted == false)
           .ToListAsync();
            return subjects!;
        }

        public async Task<StudentSubject> GetSubject(string Id)
        {
            var subject = await _context.Set<StudentSubject>()
           .Include(a => a.Student)
           .ThenInclude(a => a.User)
           .Include(a => a.Subject)
           .FirstOrDefaultAsync(a => a.Id == Id && a.IsDeleted == false);
            return subject!;
        }
    }
}
using System.Linq.Expressions;
using cbtBackend.Context;
using cbtBackend.Model.Entities;
using cbtBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace cbtBackend.Repositories.Implementations
{
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(ApplicationContext context)
        {
            _context = context;
        }

         public async Task<Question> Get(string id)
        {
            var exam = await _context.Set<Question>()
            .Include(a => a.Answers)
           .Include(a => a.Subject)
           .ThenInclude(a => a.StudentSubjects)
           .FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false);
            return exam!;
        }

        public async Task<Question> Get(Expression<Func<Question, bool>> expression)
        {
            var questions = await _context.Set<Question>()
           .Include(a => a.Subject)
           .Include(a => a.Answers)
           .FirstOrDefaultAsync(expression);
            return questions!;
        }

        public async Task<ICollection<Question>> GetAll(string subjectId, int count)
        {
            var total = await _context.Questions
            .Where(q => q.SubjectId == subjectId  && q.IsDeleted == false)
            .CountAsync();
            if (count > total) count = total;
                    var random = new Random();
                    var offsets = new HashSet<int>();
            while (offsets.Count < count)
            {
                    offsets.Add(random.Next(0, total));
            }
            var questions = new List<Question>();
            foreach (var offset in offsets)
            {
                var question = await _context.Questions
                .Where(q => q.SubjectId == subjectId)
                .Skip(offset)
                .Take(1)
                .Include(q => q.Answers.Where(a => a.IsDeleted == false))
                .FirstOrDefaultAsync();
                if (question != null)
                questions.Add(question);
            }
            return questions;
        }

        public async Task<ICollection<Question>> GetAll(Expression<Func<Question, bool>> expression)
        {
            var questions = await _context.Set<Question>()
           .Include(a => a.Subject)
           .Include(a => a.Answers)
           .Where(expression)
           .ToListAsync();
            return questions;
        }
    }
}


using System.Linq.Expressions;
using cbtBackend.Context;
using cbtBackend.Model.Entities;
using cbtBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace cbtBackend.Repositories.Implementations
{
    public class AnswerRepository : BaseRepository<Answer>, IAnswerRepository
    {
        public AnswerRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Answer> Get(string id)
        {
            var answer = await _context.Set<Answer>()
           .Include(a => a.Question)
           .ThenInclude(a => a.Answers)
           .FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false);
            return answer!;
        }

        public Task<Answer> Get(Expression<Func<Answer, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Answer>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Answer>> GetAll(Expression<Func<Answer, bool>> expression)
        {
            var answer = await _context.Set<Answer>()
           .Include(a => a.Question)
           .Where(expression)
           .ToListAsync();
            return answer!;
        }
    }
}
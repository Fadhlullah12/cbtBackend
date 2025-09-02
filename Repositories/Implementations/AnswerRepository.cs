using System.Linq.Expressions;
using cbtBackend.Context;
using cbtBackend.Model.Entities;
using cbtBackend.Repositories.Interfaces;

namespace cbtBackend.Repositories.Implementations
{
    public class AnswerRepository : BaseRepository<Answer>, IAnswerRepository
    {
        public AnswerRepository(ApplicationContext context)
        {
            _context = context;
        }

        public Task<Answer> Get(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Answer> Get(Expression<Func<Answer, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Answer>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Answer>> GetAll(Expression<Func<Answer, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
using System.Linq.Expressions;
using cbtbackend.Repositories.Interface;
using cbtBackend.Model.Entities;

namespace cbtBackend.Repositories.Interfaces
{
    public interface IQuestionRepository : IBaseRepository<Question>
    {
        Task<Question> Get(string id);
        Task<Question> Get(Expression<Func<Question, bool>> expression);
        Task<ICollection<Question>> GetAll(string subjectId, int count);
        Task<ICollection<Question>> GetAll(Expression<Func<Question, bool>> expression);
    }
}
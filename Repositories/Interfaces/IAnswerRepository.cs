using System.Linq.Expressions;
using cbtbackend.Repositories.Interface;
using cbtBackend.Model.Entities;

namespace cbtBackend.Repositories.Interfaces
{
    public interface IAnswerRepository : IBaseRepository<Answer>
    {
        Task<Answer> Get(string id);
        Task<Answer> Get(Expression<Func<Answer, bool>> expression);
        Task<ICollection<Answer>> GetAll();
        Task<ICollection<Answer>> GetAll(Expression<Func<Answer, bool>> expression);
    }
}
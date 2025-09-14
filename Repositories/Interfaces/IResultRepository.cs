using System.Linq.Expressions;
using cbtbackend.Repositories.Interface;
using cbtBackend.Model;

namespace cbtBackend.Repositories.Interfaces
{
    public interface IResultRepository : IBaseRepository<Result>
    {
        Task<Result> Get(string id);
        Task<Result> Get(Expression<Func<Result, bool>> expression);
        Task<ICollection<Result>> GetAll(Expression<Func<Result, bool>> expression);
    }
}
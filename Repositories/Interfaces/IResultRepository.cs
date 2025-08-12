using System.Linq.Expressions;
using cbtBackend.Model;

namespace cbtBackend.Repositories.Interfaces
{
    public interface IResultRepository
    {
        Task<Result> Get(string id);
        Task<Result> Get(Expression<Func<Result, bool>> expression);
        Task<ICollection<Result>> GetAll(); 
    }
}
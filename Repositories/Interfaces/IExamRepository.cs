using System.Linq.Expressions;
using cbtbackend.Repositories.Interface;
using cbtBackend.Model;

namespace cbtBackend.Repositories.Interfaces
{
    public interface IExamRepository : IBaseRepository<Exam>
    {
        Task<Exam> Get(string id);
        Task<Exam> Get(Expression<Func<Exam, bool>> expression);
        Task<ICollection<Exam>> GetAll();
        Task<ICollection<Exam>> GetAll(Expression<Func<Exam, bool>> expression);
    }
}
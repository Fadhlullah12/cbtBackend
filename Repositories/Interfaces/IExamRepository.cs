using System.Linq.Expressions;
using cbtBackend.Model;

namespace cbtBackend.Repositories.Interfaces
{
    public interface IExamRepository
    {
        Task<Exam> Get(string id);
        Task<Exam> Get(Expression<Func<Exam, bool>> expression);
        Task<ICollection<Exam>> GetAll(); 
    }
}
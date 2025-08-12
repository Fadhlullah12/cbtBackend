namespace cbtbackend.Repositories.Interface
{
    public interface IBaseRepository<T>
    {
        Task<T> Create(T entity);
        T Update(T entity);
        Task<int> Save();
    }
}
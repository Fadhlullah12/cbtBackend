using cbtbackend.Repositories.Interface;
using cbtBackend.Context;
using cbtBackend.Model;


namespace cbtBackend.Repositories.Interfaces
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity, new()
    {
        ApplicationContext _applicationContext;

        public async Task<T> Create(T entity)
        {
            await _applicationContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<int> Save()
        {
            return await _applicationContext.SaveChangesAsync();
        }

        public T Update(T entity)
        {
            _applicationContext.Update(entity);
            return entity;
        }

       
    }
}
using System.Linq.Expressions;

namespace DHCardHelper.Data.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter);
        Task AddAsync(T entity);
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Update(T entity);
    }
}

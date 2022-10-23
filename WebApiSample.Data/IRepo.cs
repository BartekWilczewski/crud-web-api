using System.Linq.Expressions;
using WebApiSample.Models.Filtering;

namespace WebApiSample.Data
{
    public interface IRepo<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAsync();
        Task<IEnumerable<TEntity>> GetAsync(PagingFilter paging);
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity,bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, PagingFilter paging = null);
        Task<TEntity> GetByIdAsync(int id);
        Task InsertAsync(TEntity entity);
        void Update(TEntity entity);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();

    }
}

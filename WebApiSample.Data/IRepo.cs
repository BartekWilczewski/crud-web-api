using System.Linq.Expressions;
using WebApiSample.Models;
using WebApiSample.Models.Filtering;

namespace WebApiSample.Data
{
    public interface IRepo<TEntity> where TEntity : Entity
    {
        Task<IEnumerable<TEntity?>> GetAsync();
        Task<IEnumerable<TEntity?>> GetAsync(PagingFilter paging, string includeProperties = "");
        Task<IEnumerable<TEntity?>> GetAsync(Expression<Func<TEntity?, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, PagingFilter paging = null, string includeProperties = "");
        Task<TEntity> GetByIdAsync(int id, string includeProperties = "");
        Task InsertAsync(TEntity? entity);
        void Update(TEntity entity);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();

    }
}

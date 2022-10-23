using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebApiSample.Models.Filtering;

namespace WebApiSample.Data
{
    public class DataRepository<TEntity> : IRepo<TEntity> where TEntity : class
    {
        private readonly ApiDbContext _ctx;
        private readonly DbSet<TEntity> _dbSet;

        public DataRepository(ApiDbContext ctx)
        {
            _ctx = ctx;
            _dbSet = ctx.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAsync(PagingFilter filter)
        {
            if (filter != null)
                return await _dbSet.Skip((filter.PageNo - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, PagingFilter paging = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
                query = orderBy(query);

            if (paging != null)
                query = query.Skip((paging.PageNo - 1) * paging.PageSize).Take(paging.PageSize);
            return await query.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task InsertAsync(TEntity entity)
        {
           await _dbSet.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _ctx.Entry(entity).State = EntityState.Modified;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            _dbSet.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiSample.Models;

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

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task InsertAsync(TEntity entity)
        {
           await _dbSet.AddAsync(entity);
           await _ctx.SaveChangesAsync();
        }

        public void Update(TEntity entity)
        {
            _ctx.Entry(entity).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            _dbSet.Remove(entity);
            await _ctx.SaveChangesAsync();

        }
    }
}

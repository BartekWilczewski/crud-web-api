using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiSample.Models;

namespace WebApiSample.Data
{
    public interface IRepo<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task InsertAsync(TEntity entity);
        void Update(TEntity entity);
        Task DeleteAsync(int id);

    }
}

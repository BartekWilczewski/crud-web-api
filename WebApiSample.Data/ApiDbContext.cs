using Microsoft.EntityFrameworkCore;
using WebApiSample.Models;

namespace WebApiSample.Data
{
    public class ApiDbContext: DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> opts) : base(opts)
        {
            
        }

        public DbSet<Book> Books { get; set; } = null;
    }
}

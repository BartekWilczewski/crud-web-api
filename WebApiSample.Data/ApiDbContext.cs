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
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Author>()
            //    .HasMany<Book>(a => a.Books);
        }
    }
}

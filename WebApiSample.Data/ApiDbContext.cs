using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

using WebApiSample.Models;

namespace WebApiSample.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApiDbContext _ctx;
        public UnitOfWork(ApiDbContext ctx, IRepo<Book> bookRepo)
        {
            _ctx = ctx;
            BookRepo = bookRepo;
        }
        public virtual void Dispose(bool disposing)
        {
            if(disposing)
                _ctx.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IRepo<Book> BookRepo { get; }
        public async Task SaveChangesAsync()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}

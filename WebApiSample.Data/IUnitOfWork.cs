using WebApiSample.Models;

namespace WebApiSample.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IRepo<Book?> BookRepo { get; }
        Task SaveChangesAsync();
    }
}

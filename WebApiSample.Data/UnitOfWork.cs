using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiSample.Models;

namespace WebApiSample.Data
{
    public class UnitOfWork
    {
        private readonly ApiDbContext _ctx;
        private IRepo<Book> _bookRepo;
        public UnitOfWork(ApiDbContext ctx)
        {
            _ctx = ctx;
        }

        public IRepo<Book> BookRepository => _bookRepo == null ? new DataRepository<Book>(_ctx) : _bookRepo;
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebApiSample.Data;
using WebApiSample.Filters;
using WebApiSample.Models;
using WebApiSample.Models.Filtering;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private IMemoryCache _cache;

        private MemoryCacheEntryOptions _cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
            .SetPriority(CacheItemPriority.Normal)
            .SetSize(1000);

        public BooksController(IUnitOfWork uow, IMemoryCache cache)
        {
            _uow = uow;
            _cache = cache;
        }

        [HttpPost]
        [TraceLog]
        public async Task<ActionResult<Book>> AddBook(Book book)
        {
            await _uow.BookRepo.InsertAsync(book);
            await _uow.SaveChangesAsync();
            return CreatedAtAction(nameof(AddBook), new {Id = book.Id}, book);
        }


        // GET: api/<BooksController>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PagingFilter filter)
        {
            IEnumerable<Book> data;
            if (!_cache.TryGetValue(CacheKeys.BookListCacheKey, out data))
            {
                data = await _uow.BookRepo.GetAsync(filter);
                if(data.Any())
                    _cache.Set(CacheKeys.BookListCacheKey, data, _cacheEntryOptions);
            }

            return Ok(new PagedResponse<IEnumerable<Book>>(data, filter.PageNo, filter.PageSize));
        }

        [HttpGet("titleby/{word}/{pageNo:int?}/{pageSize:int?}")]
        public async Task<IActionResult> GetAll(string word, int? pageNo, int? pageSize)
        {
            var filter = new PagingFilter(pageNo, pageSize);
            var data = await _uow.BookRepo.GetAsync(null, books => books.OrderBy(b => b.Author), filter);
            return Ok(new PagedResponse<IEnumerable<Book>>(data, filter.PageNo, filter.PageSize));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Book> Get(int id)
        {
            return await _uow.BookRepo.GetByIdAsync(id);
        }


        /// <summary>
        /// Deletes a specific book
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task Delete(int id)
        { 
            await _uow.BookRepo.DeleteAsync(id);
            await _uow.SaveChangesAsync();
        }

        [HttpPut]
        public async Task Put(Book book)
        {
            _uow.BookRepo.Update(book);
            await _uow.SaveChangesAsync();
        }

    }
}

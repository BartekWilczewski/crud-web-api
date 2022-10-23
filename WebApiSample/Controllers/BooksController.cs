using Microsoft.AspNetCore.Mvc;
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

        public BooksController(IUnitOfWork uow)
        {
            _uow = uow;
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
        public async Task<IActionResult> GetAll([FromQuery] BookFilter filter)
        {
            var data = await _uow.BookRepo.GetAsync(filter);
            return Ok(new PagedResponse<IEnumerable<Book>>(data, filter.PageNo, filter.PageSize));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Book> Get(int id)
        {
            return await _uow.BookRepo.GetByIdAsync(id);
        }

        [Route("{id}")]
        public async Task Delete(int id)
        { 
            await _uow.BookRepo.DeleteAsync(id);
            await _uow.SaveChangesAsync();
        }

        
        public async Task Put(Book book)
        {
            _uow.BookRepo.Update(book);
            await _uow.SaveChangesAsync();
        }

    }
}

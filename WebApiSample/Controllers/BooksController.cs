using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using WebApiSample.Data;
using WebApiSample.Filters;
using WebApiSample.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly UnitOfWork _uow;

        public BooksController(ApiDbContext _ctx)
        {
            _uow = new UnitOfWork(_ctx);
        }

        [HttpPost]
        [TraceLog]
        public async Task<ActionResult<Book>> AddBook(Book book)
        {
            await _uow.BookRepository.InsertAsync(book);
            //return StatusCode(201, book);
            return CreatedAtAction(nameof(AddBook), new {Id = book.Id}, book);
        }


        // GET: api/<BooksController>
        [HttpGet]
        public async Task<IEnumerable<Book>> GetAll()
        {
            return await _uow.BookRepository.GetAsync();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Book> Get(int id)
        {
            return await _uow.BookRepository.GetByIdAsync(id);
        }

        [Route("{id}")]
        public async Task Delete(int id)
        { 
            await _uow.BookRepository.DeleteAsync(id);
        }

        
        public void Put(Book book)
        {
            _uow.BookRepository.Update(book);
        }

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UnitOfWork_Example.Context.WorkUnits.Interfaces;
using UnitOfWork_Example.Models;

namespace UnitOfWork_Example.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BooksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            var books = _unitOfWork.Books.GetAll().ToList();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            var book = _unitOfWork.Books.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public ActionResult<Book> CreateBook(Book book)
        {
            _unitOfWork.Books.Add(book);
            _unitOfWork.Complete();
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            _unitOfWork.Books.Update(book);
            _unitOfWork.Complete();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _unitOfWork.Books.GetById(id);
            if (book == null)
            {
                return NotFound();
            }

            _unitOfWork.Books.Delete(book);
            _unitOfWork.Complete();
            return NoContent();
        }
    }

}
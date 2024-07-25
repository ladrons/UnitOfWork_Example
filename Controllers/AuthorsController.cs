using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UnitOfWork_Example.Context.WorkUnits.Interfaces;
using UnitOfWork_Example.DTOs;
using UnitOfWork_Example.Models;

namespace UnitOfWork_Example.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Author>> GetAuthors()
        {
            var authors = _unitOfWork.Authors.GetAll().ToList();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public ActionResult<Author> GetAuthor(int id)
        {
            var author = _unitOfWork.Authors.GetById(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [HttpPost]
        public IActionResult CreateAuthor([FromBody] AuthorDTO authorDto) //Direk 'Author' nesnesini almak yerine gerekli değerleri parametre olarak alırsak, daha sonra bu parametreleri kullanarak 'Author' nesnesini oluşturabiliriz.
        {
            if (authorDto == null)
            {
                return BadRequest("Author cannot be null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var author = new Author
            {
                Name = authorDto.Name
            };

            _unitOfWork.Authors.Add(author);
            _unitOfWork.Complete();

            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            _unitOfWork.Authors.Update(author);
            _unitOfWork.Complete();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            var author = _unitOfWork.Authors.GetById(id);
            if (author == null)
            {
                return NotFound();
            }

            _unitOfWork.Authors.Delete(author);
            _unitOfWork.Complete();
            return NoContent();
        }
    }

}
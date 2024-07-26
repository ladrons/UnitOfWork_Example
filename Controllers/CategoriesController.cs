using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UnitOfWork_Example.Context.WorkUnits.Interfaces;
using UnitOfWork_Example.DTOs;
using UnitOfWork_Example.Models;

namespace UnitOfWork_Example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //-----------------------------------------------------------------------------------------------------

        [HttpGet]
        public IActionResult Get()
        {
            var categories = _unitOfWork.Categories.GetAll();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Category category = _unitOfWork.Categories.GetByIdWhitInclude(id, c => c.Products);
            if (category is null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryDTO categoryDTO)
        {
            if (categoryDTO is null)
                return BadRequest();

            Category newCategory = new Category
            {
                Name = categoryDTO.Name,
                Description = categoryDTO.Description
            };

            _unitOfWork.Categories.Add(newCategory);
            _unitOfWork.Complete();

            return CreatedAtAction(nameof(Get), new { id = newCategory.Id }, newCategory);            
        }
    }
}

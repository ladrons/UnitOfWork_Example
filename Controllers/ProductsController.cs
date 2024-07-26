using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UnitOfWork_Example.Context.WorkUnits.Interfaces;
using UnitOfWork_Example.DTOs;
using UnitOfWork_Example.Models;

namespace UnitOfWork_Example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Products
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = _unitOfWork.Products.GetAll();
            return Ok(products);
        }

        // GET: api/Products/{id}
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _unitOfWork.Products.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // POST: api/Products
        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductDTO productDTO, int categoryId)
        {
            if (productDTO is null || categoryId == 0)
                return BadRequest();

            Category findCategory = _unitOfWork.Categories.GetById(categoryId);

            Product newProduct = new Product
            {
                Name = productDTO.Name,
                Description = productDTO.Description,
                Price = productDTO.Price,

                Category = findCategory
            };

            _unitOfWork.Products.Add(newProduct);
            _unitOfWork.Complete();
            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.Id }, newProduct);
        }

        // PUT: api/Products/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] ProductDTO productDTO)
        {
            Product findProduct = _unitOfWork.Products.GetById(id);

            if (findProduct is null || findProduct.Id != id)
                return BadRequest();

            findProduct.Name = productDTO.Name;
            findProduct.Description = productDTO.Description;
            findProduct.Price = productDTO.Price;

            _unitOfWork.Products.Update(findProduct);
            _unitOfWork.Complete();
            return NoContent();
        }

        // DELETE: api/Products/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            Product findProduct = _unitOfWork.Products.GetById(id);
            if (findProduct is null)
                return NotFound();

            _unitOfWork.Products.Delete(findProduct);
            _unitOfWork.Complete();

            return NoContent();
        }
    }
}

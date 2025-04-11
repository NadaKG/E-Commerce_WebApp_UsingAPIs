using Application.DTOs;
using Application.Service;
using Application.Service_Interface;
using Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Product_CategoryController : ControllerBase
    {
        private readonly IProduct_CategoryService _product_CategoryService;
        public Product_CategoryController(IProduct_CategoryService product_CategoryService)
        {
            _product_CategoryService = product_CategoryService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await _product_CategoryService.GetByIdWithProducts(id));
        }

        [HttpGet("ByName/{name}")]
        public async Task<IActionResult> GetByNameAsync(string name)
        {
            return Ok(await _product_CategoryService.GetByNameWithProducts(name));
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _product_CategoryService.GetAllAsync());
        }
        [HttpGet("GetAllWithProducts")]
        public async Task<IActionResult> GetAllWithProductsAsync()
        {
            return Ok(await _product_CategoryService.GetAllWithProductsAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Product_CategoryDto dto)
        {
           
            var product = new Product_Category
            {
                Name = dto.Name,
        Description = dto.Description,

            };
            await _product_CategoryService.Add(product);

            return Ok(product);

        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, [FromForm] Product_CategoryDto dto)
        {
            var product = await _product_CategoryService.GetById(id);

            if (product == null)
                return NotFound($"There is no product Category with ID : {id}");

            product.Name = dto.Name;
   
            product.Description = dto.Description;

            _product_CategoryService.Update(product);

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _product_CategoryService.GetById(id);

            if (product == null)
                return NotFound($"There is no product with ID : {id}");

            _product_CategoryService.Delete(product);

            return Ok(product);
        }
    }
}

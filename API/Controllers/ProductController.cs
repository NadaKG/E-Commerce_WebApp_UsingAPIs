using Application.DTOs;
using Application.Service;
using Application.Service_Interface;
using Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IPhotoService photoService;
        private long _maxAllowedImageSize = 1048576;
        public ProductController(IProductService productService, IPhotoService photoService)
        {
            _productService = productService;
            this
                 .photoService = photoService;

        }
        [HttpGet("ById/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await _productService.GetByAllId(id));
        }

        [HttpGet("ByName/{name}")]
        public async Task<IActionResult> GetByNameAsync(string name)
        {
            return Ok(await _productService.GetByName(name));
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _productService.GetAllAsync());
        }
        [HttpGet("GetCount")]
        public async Task<IActionResult> GetCountAllAsync()
        {
            return Ok(await _productService.GetCountOfProducts());
        }
        [HttpGet("GetAllWithDiscounts")]

        public async Task<IActionResult> GetAllWithDiscountsAsync()
        {
            return Ok(await _productService.GetAllProductsWithDiscountsAsync());
        }
        [HttpGet("GetAllBundles")]

        public async Task<IActionResult> GetAllBundles()
        {
            return Ok(await _productService.GetAllBundlesAsync());
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductDto dto)
        {
            var dataStreamPhoto = await photoService.AddPhotoAsync(dto.ImageUrl);

            if (dataStreamPhoto.Error != null)
                return BadRequest("you SHOULD upload a photo");

            if (dataStreamPhoto.Length > _maxAllowedImageSize)
            {
                return BadRequest("MAX allowed Size for Image is 1 MB");
            }
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Brand = dto.Brand,
                Description = dto.Description,
                ImageUrl = dataStreamPhoto.Uri.ToString(),
                CategoryId = dto.CategoryId,
                DiscountId = dto.DiscountId,

            };
            await _productService.Add(product);

            return Ok(product);

        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, [FromForm] ProductDto dto)
        {
            var product = await _productService.GetById(id);

            if (product == null)
                return NotFound($"There is no product with ID : {id}");

            product.Name = dto.Name;
            product.Price = dto.Price;
            product.Brand = dto.Brand;
            product.Description = dto.Description;

            _productService.Update(product);

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetById(id);

            if (product == null)
                return NotFound($"There is no product with ID : {id}");

            _productService.Delete(product);

            return Ok(product);
        }

    }
}

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
    public class Product_InventoryController : ControllerBase
    {
        private readonly IProduct_InventoryService _product_InventoryService;
        public Product_InventoryController(IProduct_InventoryService product_InventoryService)
        {
            _product_InventoryService = product_InventoryService;

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await _product_InventoryService.GetById(id));
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _product_InventoryService.GetAllAsync());
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Product_InventoryDto dto)
        {

            var product = new Product_Inventory
            { ProductId = dto.ProductId,
                Quantity = dto.Quantity,

            };
            await _product_InventoryService.Add(product);

            return Ok(product);

        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, [FromForm] Product_InventoryDto dto)
        {
            var product = await _product_InventoryService.GetById(id);

            if (product == null)
                return NotFound($"There is no product Inventory with ID : {id}");

            {
                product.ProductId = dto.ProductId;
                product.Quantity = dto.Quantity;

                _product_InventoryService.Update(product);

                return Ok(product);
            }
        }
            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                var product = await _product_InventoryService.GetById(id);

                if (product == null)
                    return NotFound($"There is no product with ID : {id}");

                _product_InventoryService.Delete(product);

                return Ok(product);
            }
        }
    } 

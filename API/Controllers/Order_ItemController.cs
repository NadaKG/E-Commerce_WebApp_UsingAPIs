using Application.DTOs;
using Application.Service_Interface;
using Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Order_ItemController : ControllerBase
    {
        private readonly IOrder_ItemService _order_ItemService;
        public Order_ItemController(IOrder_ItemService order_ItemService)
        {
            _order_ItemService = order_ItemService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await _order_ItemService.GetById(id));
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _order_ItemService.GetAllAsync());
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Order_ItemDto dto)
        {
            var rate = new Order_Item
            {
                ProductId = dto.ProductId,
             Quantity = dto.Quantity,

            };
            await _order_ItemService.Add(rate);

            return Ok(rate);

        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, [FromForm] Order_ItemDto dto)
        {
            var product = await _order_ItemService.GetById(id);

            if (product == null)
                return NotFound($"There is no Rate with ID : {id}");

            product.ProductId = dto.ProductId;
            product.Quantity = dto.Quantity;


            _order_ItemService.Update(product);

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _order_ItemService.GetById(id);

            if (product == null)
                return NotFound($"There is no Rate with ID : {id}");

            _order_ItemService.Delete(product);

            return Ok(product);
        }
    }
}

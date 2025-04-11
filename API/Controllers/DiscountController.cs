using Application.DTOs;
using Application.Service_Interface;
using Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {

        private readonly IDiscountService _discountService;
        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await _discountService.GetDiscountByIdAsync(id));
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _discountService.GetAllDiscountsAsync());
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] DiscountDto dto)
        {

            var product = new Discount
            {
                Discount_Percent = dto.Discount_Percent,
                IsActive = dto.IsActive,

            };
            await _discountService.CreateDiscountAsync(product);

            return Ok(product);

        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, [FromForm] DiscountDto dto)
        {
            var product = await _discountService.GetDiscountByIdAsync(id);

            if (product == null)
                return NotFound($"There is no product Category with ID : {id}");

            product.Discount_Percent = dto.Discount_Percent;

            product.IsActive = dto.IsActive;

            _discountService.UpdateDiscountAsync(product);

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _discountService.GetDiscountByIdAsync(id);

            if (product == null)
                return NotFound($"There is no product with ID : {id}");

            _discountService.DeleteDiscountAsync(id);

            return Ok(product);
        }
    }
}

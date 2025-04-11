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
    public class RateController : ControllerBase
    {
        private readonly IRateService _rateService;
        public RateController(IRateService rateService)
        {
            _rateService = rateService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await _rateService.GetById(id));
        }
        [HttpGet("ByProductId/{id}")]
        public async Task<IActionResult> GetByProductIdAsync(int id)
        {
            return Ok(await _rateService.GetByProductId(id));
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _rateService.GetAllAsync());
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] RateDto dto)
        {
            var rate = new Rate
            {
                ProductId = dto.ProductId,
                UserId = dto.UserId,
                Value = dto.Value,
                DateTime = DateTime.Now,
                comment = dto.comment
            };
            await _rateService.Add(rate);

            return Ok(rate);

        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, [FromForm] RateDto dto)
        {
            var product = await _rateService.GetById(id);

            if (product == null)
                return NotFound($"There is no Rate with ID : {id}");

            product.ProductId = dto.ProductId;
            product.UserId = dto.UserId;
            product.Value = dto.Value;

            _rateService.Update(product);

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _rateService.GetById(id);

            if (product == null)
                return NotFound($"There is no Rate with ID : {id}");

            _rateService.Delete(product);

            return Ok(product);
        }
    }
}

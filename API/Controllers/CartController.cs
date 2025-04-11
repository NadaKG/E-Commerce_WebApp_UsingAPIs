using Application.Service_Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart(int userId, int productId, int quantity)
        {
            await _cartService.AddToCartAsync(userId, productId, quantity);
            return Ok();
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(int userId)
        {
            var cart = await _cartService.GetCartWithDetailsAsync(userId);
            return Ok(cart);
        }

        [HttpDelete("remove/{userId}/{productId}")]
        public async Task<IActionResult> RemoveFromCart(int userId, int productId)
        {
            await _cartService.RemoveFromCartAsync(userId, productId);
            return Ok();
        }

        [HttpDelete("clear/{userId}")]
        public async Task<IActionResult> ClearCart(int userId)
        {
            await _cartService.ClearCartAsync(userId);
            return Ok();
        }
    }
}

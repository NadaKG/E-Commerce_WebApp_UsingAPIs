using Application.DTOs;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service_Interface
{
    public interface ICartService
    {
        //void AddToCart(string userId, CartItem cartItem);`    

        Task<Cart> GetCartAsync(int userId);
        Task<CartDto> GetCartWithDetailsAsync(int userId);
        Task AddToCartAsync(int userId, int productId, int quantity);
        Task RemoveFromCartAsync(int userId, int productId);
        Task ClearCartAsync(int userId);
    }
}

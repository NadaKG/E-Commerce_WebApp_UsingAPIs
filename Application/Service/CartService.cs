using Application.DTOs;
using Application.Service_Interface;
using Domain.Model;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class CartService:ICartService
    {
        private readonly ApplicationDbContext _context;

        public CartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Cart> GetCartAsync(int userId)
        {
            var cart = await _context.Cart
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            return cart;
        }  
        public async Task<CartDto> GetCartWithDetailsAsync(int userId)
        {
            var cart = await _context.Cart
                .Include(c => c.Items).ThenInclude(c=>c.Products)
                .FirstOrDefaultAsync(c => c.UserId == userId);
            var cartDto = new CartDto
            {
                UserId = cart.UserId,
                Items = cart.Items.Select(item => new CartItemDto
                {
                    ProductId = item.ProductId,
                    Name = item.Products.Name,
                    Price = item.Products.Price,
                    Quantity = item.Quantity,
                    ImageUrl=item.Products.ImageUrl

                }).ToList()
            };

            return cartDto;
        }

        public async Task AddToCartAsync(int userId, int productId, int quantity)
        {
            var cart = await GetCartAsync(userId); 

            if (cart == null)
            {
                cart = new Cart { UserId = userId, Items = new List<CartItem>() };
                _context.Cart.Add(cart);
            }

            var existingItem = cart.Items.FirstOrDefault(item => item.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                var product = await _context.Products.FindAsync(productId); 
                if (product != null)
                {
                    var cartItem = new CartItem
                    {
                        ProductId = productId,
                        Quantity = quantity,
                        UserId =userId,
                    };
                    cart.Items.Add(cartItem);
                }

            }

            await _context.SaveChangesAsync();
        }

        public async Task RemoveFromCartAsync(int userId, int productId)
        {
            var cart = await GetCartAsync(userId); 

            var itemToRemove = cart.Items.FirstOrDefault(item => item.ProductId == productId);
            if (itemToRemove != null)
            {
                cart.Items.Remove(itemToRemove);
                await _context.SaveChangesAsync(); 
            }
        }

        public async Task ClearCartAsync(int userId)
        {
            var cart = await GetCartAsync(userId); 
            cart.Items.Clear();
            await _context.SaveChangesAsync(); 
        }
    }
}

        //public void AddToCart(string userId, CartItem cartItem)
        //{
        //    var cart = GetCart(userId);

        //    if (cart == null)
        //    {
        //        cart = new Cart { UserId = userId, Items = new List<CartItem>() };
        //        _context.Cart.Add(cart);
        //    }

        //    var existingItem = cart.Items.FirstOrDefault(item => item.ProductId == cartItem.ProductId);
        //    if (existingItem != null)
        //    {
        //        existingItem.Quantity += cartItem.Quantity;
        //    }
        //    else
        //    {
        //        cart.Items.Add(cartItem);
        //    }

        //    _context.SaveChanges();
        //}
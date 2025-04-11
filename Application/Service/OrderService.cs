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
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICartService _cartService;
        private readonly IOrderNumberGenerator _orderNumberGenerator;

        public OrderService(ApplicationDbContext context, ICartService cartService, IOrderNumberGenerator orderNumberGenerator)
        {
            _context = context;
            _cartService = cartService;
            _orderNumberGenerator = orderNumberGenerator;
        }



        public async Task<OrderDto> PlaceOrderAsync(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }

            var cart = await _cartService.GetCartWithDetailsAsync(userId);
            if (cart == null || !cart.Items.Any())
            {
                throw new ArgumentException("Cart is empty.");
            }

            var orderNumber = _orderNumberGenerator.Generate();
            var order = new Order
            {
                OrderNumber = orderNumber,
                OrderDate = DateTime.UtcNow,
                UserId = userId,
                TotalAmount = 500,
                Status = "Pending",
                OrderDetails = new List<Order_Detail>()
            };

            _context.Order.Add(order);
            await _context.SaveChangesAsync();

            foreach (var item in cart.Items)
            {
                var orderDetail = new Order_Detail
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    PaymentId = 1,
                };

                order.OrderDetails.Add(orderDetail);
            }

            foreach (var orderDetail in order.OrderDetails)
            {
                orderDetail.OrderId = order.Id;
            }

            await _context.SaveChangesAsync();

            await _cartService.ClearCartAsync(userId);

            // Map Order entity to OrderDto
            var orderDto = new OrderDto
            {
                OrderNumber = order.OrderNumber,
                OrderDate = order.OrderDate,
                UserId = order.UserId,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                // Map other properties
                OrderDetails = order.OrderDetails.Select(od => new OrderDetailDto
                {
                    ProductId = od.ProductId,
                    Quantity = od.Quantity,
                    Price = od.Price,
                    PaymentId = od.PaymentId
                }).ToList()
            };

            return orderDto;
        }

        //public async Task<Order> PlaceOrderAsync(int userId)
        //{
        //    var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        //    if (user == null)
        //    {
        //        throw new ArgumentException("User not found.");
        //    }

        //    var cart = await _cartService.GetCartWithDetailsAsync(userId);
        //    if (cart == null || !cart.Items.Any())
        //    {
        //        throw new ArgumentException("Cart is empty.");
        //    }

        //    var orderNumber = _orderNumberGenerator.Generate();
        //    var order = new Order
        //    {
        //        OrderNumber = orderNumber,
        //        OrderDate = DateTime.UtcNow,
        //        UserId = userId,
        //        TotalAmount = 500,
        //        Status = "Pending",
        //        OrderDetails = new List<Order_Detail>() 
        //    };


        //    _context.Order.Add(order);
        //    await _context.SaveChangesAsync();
        //    foreach (var item in cart.Items)
        //    {
        //        var orderDetail = new Order_Detail
        //        { OrderId = order.Id,
        //            ProductId = item.ProductId,
        //            Quantity = item.Quantity,
        //            Price = item.Price,
        //            PaymentId=1,

        //        };

        //        order.OrderDetails.Add(orderDetail);
        //    }



        //    foreach (var orderDetail in order.OrderDetails)
        //    {
        //        orderDetail.OrderId = order.Id;
        //    }

        //    await _context.SaveChangesAsync();

        //    await _cartService.ClearCartAsync(userId);

        //    return order;
        //}




        public async Task<IEnumerable<OrderDto>> GetByUserId(int userId)
        {
            var orders = await _context.Order
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .ToListAsync();

            // Map orders to OrderDto
            var orderDtos = orders.Select(order => new OrderDto
            {
                OrderNumber = order.OrderNumber,
                OrderDate = order.OrderDate,
                UserId = order.UserId,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                OrderDetails = order.OrderDetails.Select(od => new OrderDetailDto
                {
                    ProductId = od.ProductId,
                    Quantity = od.Quantity,
                    Price = od.Price,
                    PaymentId = od.PaymentId
                }).ToList()
            });

            return orderDtos.ToList();
        }





    }
}

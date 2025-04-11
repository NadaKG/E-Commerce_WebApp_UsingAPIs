using Application.DTOs;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service_Interface
{
    public interface IOrderService
    {
        //Task<Order> PlaceOrderAsync(int userId);
        Task<OrderDto> PlaceOrderAsync(int userId);
        //Task<List<Order>> GetByUserId(int userId);
        Task<IEnumerable<OrderDto>> GetByUserId(int userId);   
            
            
            }
}

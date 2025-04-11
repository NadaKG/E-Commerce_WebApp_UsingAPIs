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
    public class Order_ItemService: IOrder_ItemService
    {
        private readonly ApplicationDbContext _context;
        public Order_ItemService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Order_Item> Add(Order_Item order)
        {
            await _context.Order_Items.AddAsync(order);

            _context.SaveChanges();
            return order;
        }

        public Order_Item Delete(Order_Item order)
        {
            _context.Remove(order);
            _context.SaveChanges();
            return order;
        }

        public async Task<IEnumerable<Order_Item>> GetAllAsync()
        {
            return await _context.Order_Items.ToListAsync();
        }

        public async Task<Order_Item> GetById(int id)
        {
            return await _context.Order_Items.SingleOrDefaultAsync(p => p.Id == id);
        }

        public Order_Item Update(Order_Item order)
        {
            _context.Update(order);
            _context.SaveChanges();
            return order;
        }
    }
}

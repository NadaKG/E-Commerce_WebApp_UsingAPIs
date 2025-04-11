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
    public class Order_DetailService : IOrder_DetailService
    {
        private readonly ApplicationDbContext _context;
        public Order_DetailService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Order_Detail> Add(Order_Detail order)
        {
            await _context.Order_Details.AddAsync(order);

            _context.SaveChanges();
            return order;
        }

        public Order_Detail Delete(Order_Detail order)
        {
            _context.Remove(order);
            _context.SaveChanges();
            return order;
        }

        public async Task<IEnumerable<Order_Detail>> GetAllAsync()
        {
            return await _context.Order_Details.ToListAsync();
        }

        public async Task<Order_Detail> GetById(int id)
        {
            return await _context.Order_Details.SingleOrDefaultAsync(p => p.Id == id);
        }

        public Order_Detail Update(Order_Detail order)
        {
            _context.Update(order);
            _context.SaveChanges();
            return order;
        }
    }
}

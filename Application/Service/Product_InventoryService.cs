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
    public class Product_InventoryService: IProduct_InventoryService
    {
        private readonly ApplicationDbContext _context;
        public Product_InventoryService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Product_Inventory> Add(Product_Inventory product)
        {
            var existingProduct = await _context.Product_Inventories
                                                .SingleOrDefaultAsync(p => p.ProductId == product.ProductId);

            if (existingProduct == null)
            {
                await _context.Product_Inventories.AddAsync(product);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Product already exists in the inventory.");
            }

            return product;
        }


        public Product_Inventory Delete(Product_Inventory product)
        {
            _context.Remove(product);
            _context.SaveChanges();
            return product;
        }

        public async Task<IEnumerable<Product_Inventory>> GetAllAsync()
        {
            return await _context.Product_Inventories.ToListAsync();
        }

        public async Task<Product_Inventory> GetById(int id)
        {
            return await _context.Product_Inventories.SingleOrDefaultAsync(p => p.Id == id);
        }

        public Product_Inventory Update(Product_Inventory product)
        {
            _context.Update(product);
            _context.SaveChanges();
            return product;
        }
    }
}

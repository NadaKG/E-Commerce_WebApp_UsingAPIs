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
    public class RateService : IRateService
    {
        private readonly ApplicationDbContext _context;
        public RateService(ApplicationDbContext context)
        {
            _context = context;
        }
  public async Task<Rate> Add(Rate product)
{
    var existingRate = await _context.Rates
        .FirstOrDefaultAsync(r => r.UserId == product.UserId && r.ProductId == product.ProductId);

    if (existingRate != null)
    {
        throw new InvalidOperationException("A rating for this product by this user already exists.");
    }

    await _context.Rates.AddAsync(product);
    _context.SaveChanges();
    return product;
}

        public Rate Delete(Rate product)
        {
            _context.Remove(product);
            _context.SaveChanges();
            return product;
        }

        public async Task<IEnumerable<RateGetDto>> GetAllAsync()
        {
            var rates = await _context.Rates.Include(u=>u.User).Include(p=>p.Product).ToListAsync();
            var rateDtos = rates.Select(rate => new RateGetDto
            {
                ProductId = rate.ProductId,
                ProductName = rate.Product.Name,
                Description = rate.Product.Description,
                ImageUrl = rate.Product.ImageUrl,
                Price = rate.Product.Price,
                UserId = rate.UserId,
                Name=rate.User.UserName,
                Value = rate.Value,
                comment = rate.comment,
                Time = rate.DateTime,
                
            });

            return rateDtos;
        }

        public async Task<Rate> GetById(int id)
        {
            return await _context.Rates.SingleOrDefaultAsync(p => p.Id == id);
        }
        public async Task<RateGetDto> GetByProductId(int id)
        {
            var rate = await _context.Rates
                .Include(r => r.Product)
                .Include(r => r.User)
                .SingleOrDefaultAsync(p => p.ProductId == id);
            var rateDto=new RateGetDto
             { 
                ProductId = rate.ProductId,
                ProductName=rate.Product.Name,
                Description=rate.Product.Description,
                ImageUrl=rate.Product.ImageUrl,
                Price=rate.Product.Price,
                UserId = rate.UserId,
                 Name = rate.User.UserName,
                 Value = rate.Value,
                comment =rate.comment,
                Time=rate.DateTime
                
            };
            return rateDto;
        }
        public Rate Update(Rate product)
        {
            _context.Update(product);
            _context.SaveChanges();
            return product;
        }
    }
}

using Application.DTOs;
using Application.Service_Interface;
using CloudinaryDotNet.Core;
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
    public class Product_CategoryService : IProduct_CategoryService
    {
        private readonly ApplicationDbContext _context;
        public Product_CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Product_Category> Add(Product_Category product)
        {
            await _context.Product_Categories.AddAsync(product);

            _context.SaveChanges();
            return product;
        }

        public Product_Category Delete(Product_Category product)
        {
            _context.Remove(product);
            _context.SaveChanges();
            return product;
        }

        public async Task<IEnumerable<Product_CategoryDto>> GetAllAsync()
        {
            var product = await _context.Product_Categories.ToListAsync();
            var productDTOs = product.Select(p => new Product_CategoryDto {
                Description = p.Description,
                Name = p.Name,
                id=p.Id


            });

            return productDTOs;
        }
        public async Task<IEnumerable<Product_Category>> GetAllWithProductsAsync()
        {
            return await _context.Product_Categories.Include(p=>p.Products).ThenInclude(d=>d.Discount).ToListAsync();
        }


        public async Task<ProductCategoryDetailsDto> GetByIdWithProducts(int id)
        {
            var product = await _context.Product_Categories
                          .Include(pc => pc.Products)
                          .ThenInclude(p => p.Discount) 
                        .Include(pc => pc.Products)
                            .ThenInclude(p => p.Rates) 
                   .Include(pc => pc.Products)
                         .ThenInclude(p => p.Inventory) 
                  .SingleOrDefaultAsync(C => C.Id == id);

            var productDTO = new ProductCategoryDetailsDto
            {
                Description = product.Description,
                Name = product.Name,
                id=product.Id,
                Products = product.Products.Select(product => new ProductWithDiscountDTO
                {
                    Description = product.Description,
                    Name = product.Name,
                    ImageUrl = product.ImageUrl,
                    Brand = product.Brand,
                    CategoryId = product.CategoryId,
                    Inventory = product.Inventory?.Quantity ?? 0,
                    Price = product.Price,
                    DiscountedPrice = product.Discount != null ? product.Price - (product.Price * (product.Discount.Discount_Percent) / 100M) : product.Price,
                    Discount_Percent = product.Discount?.Discount_Percent ?? 0,
                    Rates = product.Rates?.Select(r => new RateDto
                    {
                        Value = r.Value,
                        UserId = r.UserId,
                        ProductId = r.ProductId
                    }).ToList() ?? new List<RateDto>()

                }).ToList()
            };

            return productDTO;
        }
        public async Task<ProductCategoryDetailsDto> GetByNameWithProducts(string name)
        {
            var product = await _context.Product_Categories
                          .Include(pc => pc.Products)
                          .ThenInclude(p => p.Discount)
                        .Include(pc => pc.Products)
                            .ThenInclude(p => p.Rates)
                   .Include(pc => pc.Products)
                         .ThenInclude(p => p.Inventory)
        .Where(p => p.Name.StartsWith(name)).FirstOrDefaultAsync(); ;

            var productDTO = new ProductCategoryDetailsDto
            {
                Description = product.Description,
                Name = product.Name,
                id = product.Id,
                Products = product.Products.Select(product => new ProductWithDiscountDTO
                {
                    Description = product.Description,
                    Name = product.Name,
                    ImageUrl = product.ImageUrl,
                    Brand = product.Brand,
                    CategoryId = product.CategoryId,
                    Inventory = product.Inventory?.Quantity ?? 0,
                    Price = product.Price,
                    DiscountedPrice = product.Discount != null ? product.Price - (product.Price * (product.Discount.Discount_Percent) / 100M) : product.Price,
                    Discount_Percent = product.Discount?.Discount_Percent ?? 0,
                    Rates = product.Rates?.Select(r => new RateDto
                    {
                        Value = r.Value,
                        UserId = r.UserId,
                        ProductId = r.ProductId
                    }).ToList() ?? new List<RateDto>()

                }).ToList()
            };

            return productDTO;
        }

        public async Task<Product_Category> GetById(int id)
        {
            return await _context.Product_Categories.SingleOrDefaultAsync(p => p.Id == id);
        }
        public Product_Category Update(Product_Category product)
        {
            _context.Update(product);
            _context.SaveChanges();
            return product;
        }
    }
}

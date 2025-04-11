using Application.DTOs;
using Application.Service_Interface;
using Domain.Interface;
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
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Product> Add(Product product)
        {
            await _context.Products.AddAsync(product);

            _context.SaveChanges();
            return product;
        }
        public async Task<int> GetCountOfProducts()
        {
            return await _context.Products.CountAsync();

        }

        public Product Delete(Product product)
        {
            _context.Remove(product);
            _context.SaveChanges();
            return product;
        }

        public async Task<IEnumerable<ProductWithDiscountDTO>> GetAllAsync()
        {
            var products = await _context.Products.Include(r => r.Rates)
                .Include(c => c.Category)
                .Include(d => d.Discount)
                .Include(i => i.Inventory)
                .ToListAsync();
            if (products.Any(p => p.DiscountId != null && p.Discount.IsActive == true))
            {
                var productDTOs = products.Select(p => new ProductWithDiscountDTO
                {
                    id = p.Id,
                    Description = p.Description,
                    Name = p.Name,
                    Price = p.Price,
                    DiscountedPrice = p.Discount != null ? p.Price - (p.Price * (p.Discount.Discount_Percent) / 100M) : p.Price,
                    ImageUrl = p.ImageUrl,
                    Discount_Percent = p.Discount?.Discount_Percent ?? 0,
                    Brand = p.Brand,
                    CategoryId = p.CategoryId,
                    Rates = p.Rates?.Select(r => new RateDto
                    {
                        Value = r.Value,
                        UserId = r.UserId,
                        ProductId = r.ProductId
                    }).ToList(),
                    Inventory = p.Inventory != null ? p.Inventory.Quantity : 0
                });

                return productDTOs;
            }
            else
            {
                var productDTOs = products.Select(p => new ProductWithDiscountDTO
                {
                    id = p.Id,
                    Description = p.Description,
                    Name = p.Name,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    Brand = p.Brand,
                    CategoryId = p.CategoryId,
                    Rates = p.Rates?.Select(r => new RateDto
                    {
                        Value = r.Value,
                        UserId = r.UserId,
                        ProductId = r.ProductId
                    }).ToList(),
                    Inventory = p.Inventory != null ? p.Inventory.Quantity : 0
                });

                return productDTOs;
            }



        }
        public async Task<IEnumerable<ProductWithDiscountDTO>> GetAllProductsWithDiscountsAsync()
        {
            var products = await _context.Products
                .Include(p => p.Discount)
                .Include(p => p.Rates)
                .Include(p => p.Inventory)
                .Include(p => p.Category)
                .Where(p => p.DiscountId != null && p.Discount.IsActive == true).OrderByDescending(p => p.Discount.Discount_Percent)
                .ToListAsync();

            var productDTOs = products.Select(p => new ProductWithDiscountDTO
            {
                id = p.Id,
                Description = p.Description,
                Name = p.Name,
                Price = p.Price,
                DiscountedPrice = p.Discount != null ? p.Price - (p.Price * (p.Discount.Discount_Percent) / 100M) : p.Price,
                ImageUrl = p.ImageUrl,
                Discount_Percent = p.Discount?.Discount_Percent ?? 0,
                Brand = p.Brand,
                CategoryId = p.CategoryId,
                Rates = p.Rates?.Select(r => new RateDto
                {
                    Value = r.Value,
                    UserId = r.UserId,
                    ProductId = r.ProductId
                }).ToList(),
                Inventory = p.Inventory != null ? p.Inventory.Quantity : 0
            });


            return productDTOs;
        }
        public async Task<IEnumerable<ProductWithDiscountDTO>> GetAllBundlesAsync()
        {
            var products = await _context.Products
                .Include(p => p.Discount)
                .Include(p => p.Rates)
                .Include(p => p.Inventory)
                .Include(p => p.Category)
                .Where(p => p.Brand == "Bundle")
                .ToListAsync();

            var productDTOs = products.Select(p => new ProductWithDiscountDTO
            {
                id = p.Id,
                Description = p.Description,
                Name = p.Name,
                Price = p.Price,
                DiscountedPrice = p.Discount != null ? p.Price - (p.Price * (p.Discount.Discount_Percent) / 100M) : p.Price,
                ImageUrl = p.ImageUrl,
                Discount_Percent = p.Discount?.Discount_Percent ?? 0,
                Brand = p.Brand,
                CategoryId = p.CategoryId,
                Rates = p.Rates?.Select(r => new RateDto
                {
                    Value = r.Value,
                    UserId = r.UserId,
                    ProductId = r.ProductId
                }).ToList(),
                Inventory = p.Inventory != null ? p.Inventory.Quantity : 0
            });


            return productDTOs;
        }

        public async Task<ProductWithDiscountDTO> GetByAllId(int id)
        {
            var product = await _context.Products
                .Include(r => r.Rates)
                .Include(c => c.Category)
                .Include(d => d.Discount)
                .Include(i => i.Inventory)
                .SingleOrDefaultAsync(p => p.Id == id);



            var productDTO = new ProductWithDiscountDTO
            {
                id = product.Id,
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
            };

            return productDTO;
        }
        public async Task<ProductWithDiscountDTO> GetByName(string name)
        {
            var product = await _context.Products
                .Include(r => r.Rates)
                .Include(c => c.Category)
                .Include(d => d.Discount)
                .Include(i => i.Inventory)
        .Where(p => p.Name.StartsWith(name)).FirstOrDefaultAsync(); ;



            var productDTO = new ProductWithDiscountDTO
            {
                id = product.Id,
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
            };

            return productDTO;
        }


        public async Task<Product> GetById(int id)
        {
            return await _context.Products.SingleOrDefaultAsync(p => p.Id == id);


        }
        public Product Update(Product product)
        {
            _context.Update(product);
            _context.SaveChanges();
            return product;
        }

    }
}

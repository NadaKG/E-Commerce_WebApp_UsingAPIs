using Application.DTOs;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service_Interface
{
    public interface IProductService
    {
        Task<IEnumerable<ProductWithDiscountDTO>> GetAllProductsWithDiscountsAsync();

        Task<IEnumerable<ProductWithDiscountDTO>> GetAllAsync();
        Product Update(Product product);
        Task<Product> Add(Product product);
        Product Delete(Product product);
        Task<ProductWithDiscountDTO> GetByAllId(int id);
        Task<int> GetCountOfProducts();

        Task<Product> GetById(int id);
        Task<ProductWithDiscountDTO> GetByName(string name);
        Task<IEnumerable<ProductWithDiscountDTO>> GetAllBundlesAsync();
    }
}

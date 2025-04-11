using Application.DTOs;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service_Interface
{
    public interface IProduct_CategoryService
    {
        Task<IEnumerable<Product_CategoryDto>> GetAllAsync();
        Task<IEnumerable<Product_Category>> GetAllWithProductsAsync();
        Product_Category Update(Product_Category product);
        Task<Product_Category> Add(Product_Category product);
        Product_Category Delete(Product_Category product);
        Task<Product_Category> GetById(int id);
        Task<ProductCategoryDetailsDto> GetByIdWithProducts(int id);
        Task<ProductCategoryDetailsDto> GetByNameWithProducts(string name);
    }
}

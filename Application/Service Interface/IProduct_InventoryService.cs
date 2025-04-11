using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service_Interface
{
    public interface IProduct_InventoryService
    {
        Task<IEnumerable<Product_Inventory>> GetAllAsync();
        Product_Inventory Update(Product_Inventory product);
        Task<Product_Inventory> Add(Product_Inventory product);
        Product_Inventory Delete(Product_Inventory product);
        Task<Product_Inventory> GetById(int id);
    }
}

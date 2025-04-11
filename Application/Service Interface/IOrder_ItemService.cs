using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service_Interface
{
    public interface IOrder_ItemService
    {
        Task<IEnumerable<Order_Item>> GetAllAsync();
        Order_Item Update(Order_Item product);
        Task<Order_Item> Add(Order_Item product);
        Order_Item Delete(Order_Item product);
        Task<Order_Item> GetById(int id);
    }
}

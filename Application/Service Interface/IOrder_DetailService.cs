using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service_Interface
{
    public interface IOrder_DetailService
    {
        Task<IEnumerable<Order_Detail>> GetAllAsync();
        Order_Detail Update(Order_Detail product);
        Task<Order_Detail> Add(Order_Detail product);
        Order_Detail Delete(Order_Detail product);
        Task<Order_Detail> GetById(int id);
    }
}

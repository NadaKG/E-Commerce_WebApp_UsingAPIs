using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service_Interface
{
    public interface IDiscountService
    {
        Task<IEnumerable<Discount>> GetAllDiscountsAsync();
        Task<Discount> GetDiscountByIdAsync(int id);
        Task<Discount> CreateDiscountAsync(Discount discount);
        Task<Discount> UpdateDiscountAsync(Discount discount);
        Task<bool> DeleteDiscountAsync(int id);
    }
}

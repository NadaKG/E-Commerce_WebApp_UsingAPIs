using Application.Service_Interface;
using Domain.Interface;
using Domain.Model;
using Infra.Data.Reposatory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class DiscountService :IDiscountService
    { 
         private readonly IBaseRopository<Discount> _discountRepository;

    public DiscountService(IBaseRopository<Discount> discountRepository)
    {
        _discountRepository = discountRepository;
    }

        public async Task<IEnumerable<Discount>> GetAllDiscountsAsync()
        {
            return await _discountRepository.GetAllAsync();
        }


        public async Task<Discount> GetDiscountByIdAsync(int id)
        {
            return await _discountRepository.GetByIdAsync(id);
        }

        public async Task<Discount> CreateDiscountAsync(Discount discount)
        {
            return await _discountRepository.AddAsync(discount);
        }

       public async Task<Discount> UpdateDiscountAsync(Discount discount)
        {
            return _discountRepository.UpdateAsync(discount);
        }

        public async Task<Discount> AddDiscountAsync(Discount discount)
        {
            return await _discountRepository.AddAsync(discount);
        }
        public async Task<bool> DeleteDiscountAsync(int id)
        {
            var discountToDelete = await _discountRepository.GetByIdAsync(id);
            if (discountToDelete == null)
                return false;

            _discountRepository.DeleteAsync(discountToDelete);
            return true;
        }
    }
}

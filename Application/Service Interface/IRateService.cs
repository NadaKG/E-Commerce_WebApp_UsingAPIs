using Application.DTOs;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service_Interface
{
    public interface IRateService
    {
        Task<IEnumerable<RateGetDto>> GetAllAsync();
        Rate Update(Rate product);
        Task<Rate> Add(Rate product);
        Rate Delete(Rate product);
        Task<Rate> GetById(int id);
        Task<RateGetDto> GetByProductId(int id);
    }
}

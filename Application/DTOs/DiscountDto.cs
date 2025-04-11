using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class DiscountDto
    {
        public bool IsActive { get; set; } = false;
        public decimal Discount_Percent { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Discount
    {
        public int Id { get; set; }
        public bool IsActive { get; set; } = false;
        public decimal Discount_Percent { get; set; }


    }
}
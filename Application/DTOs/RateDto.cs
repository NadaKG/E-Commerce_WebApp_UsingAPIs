using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class RateDto
    {
        public int ProductId { get; set; }
        public string comment { get; set; }
        public double Value { get; set; }
        public int UserId { get; set; }

    }
}

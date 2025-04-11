using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class RateGetDto
    {

        public int ProductId { get; set; }
        [Range(0, 5.0)]
        public string comment { get; set; }
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public double Value { get; set; }
        public int UserId { get; set; }

    }
}

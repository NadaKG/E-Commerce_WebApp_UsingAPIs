using Domain.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ProductWithDiscountDTO
    {
        public int id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public decimal Discount_Percent { get; set; }
        public List<RateDto>?Rates { get; set; }
        public int Inventory { get; set; }
    }

}

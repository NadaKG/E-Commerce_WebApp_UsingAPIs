using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ProductCategoryDetailsDto
    {
        public int id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public List<ProductWithDiscountDTO> Products { get; set; }

    }
}

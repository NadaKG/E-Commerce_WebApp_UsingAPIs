using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public virtual Product_Inventory Inventory { get; set; }
        public int CategoryId { get; set; }
        public Product_Category Category { get; set; }
        public int? DiscountId { get; set; }
        public virtual Discount Discount { get; set; }
        public ICollection<Rate> Rates { get; set; }


    }
}

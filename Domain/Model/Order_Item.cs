using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Order_Item
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId{ get; set; }
        public Product Product { get; set; }
        public string UserId { get; set; }
        public List<Cart> Items { get; set; } 
        public decimal TotalAmount { get; set; }    
        public DateTime CreatedAt { get; set; }
    }
}

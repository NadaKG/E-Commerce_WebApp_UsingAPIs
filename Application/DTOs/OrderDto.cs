using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class OrderDto
    {
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }

        [JsonIgnore]
        public System.Threading.ExecutionContext Context { get; set; }
        public List<OrderDetailDto> OrderDetails { get; set; }
    }
}

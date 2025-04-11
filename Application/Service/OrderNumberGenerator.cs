using Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class OrderNumberGenerator : IOrderNumberGenerator
    {
        private readonly ApplicationDbContext _context;

        public OrderNumberGenerator(ApplicationDbContext context)
        {
            _context = context;
        }

        public string Generate()
        {
            var orderNumber = "ORD-";

            var existingOrderNumber = _context.Order
                .OrderByDescending(o => o.OrderNumber)
                .Select(o => o.OrderNumber)
                .FirstOrDefault();

            if (!string.IsNullOrEmpty(existingOrderNumber))
            {
                var orderNumberParts = existingOrderNumber.Split('-');
                if (orderNumberParts.Length == 2 && int.TryParse(orderNumberParts[1], out int orderNumberIndex))
                {
                    orderNumberIndex++;
                    orderNumber += orderNumberIndex.ToString().PadLeft(5, '0');
                }
                else
                {
                    // Handle unexpected order number format
                    throw new InvalidOperationException("Unexpected order number format.");
                }
            }
            else
            {
                orderNumber += "00001";
            }

            return orderNumber;
        }

    }
}



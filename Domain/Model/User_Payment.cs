using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class User_Payment
    {
        public int Id { get; set; }
        public string StripePaymentId { get; set; }
        public string PaymentStatus { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }


    }
}

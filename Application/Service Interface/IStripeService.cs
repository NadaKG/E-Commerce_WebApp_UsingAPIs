using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stripe;

namespace Application.Service_Interface
{
    public interface IStripeService
    {
        Task<PaymentIntent> CreatePaymentIntentAsync(decimal amount, string currency);
    }
}

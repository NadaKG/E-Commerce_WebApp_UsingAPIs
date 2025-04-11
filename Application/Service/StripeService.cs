using Application.Service_Interface;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class StripeService : IStripeService
    {
        public async Task<PaymentIntent> CreatePaymentIntentAsync(decimal amount, string currency)
        {
            var paymentIntentOptions = new PaymentIntentCreateOptions
            {
                Amount = (long)amount * 100,  // Convert to cents
                Currency = currency,
                PaymentMethodTypes = new List<string> { "card" },
            };

            var paymentIntentService = new PaymentIntentService();
            return await paymentIntentService.CreateAsync(paymentIntentOptions);
        }
    }
}

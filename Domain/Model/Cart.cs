using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Cart
    {

        public int Id { get; set; }
        public int UserId { get; set; }
      public ICollection<CartItem> Items { get; set; }

        //public int SessionId { get; set; }

        //public Cart_Session Session { get; set; }

    }
}

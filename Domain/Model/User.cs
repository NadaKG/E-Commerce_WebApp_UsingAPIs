using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Contact { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? LoginDate { get; set; }
        public ICollection<Rate>? Rates { get; set; }
        public ICollection<User_Address>? Addresses { get; set; }
        public ICollection<User_Payment>? Payments { get; set; }
        public List<Order>? Orders { get; set; }
        public Cart Cart { get; set; }

    }
}

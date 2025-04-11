using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Cart_Session
    {
        public int Id { get; set; }
        public string Total { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Payment_Details
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int amount { get; set; }
        public string status { get; set; }


    }
}

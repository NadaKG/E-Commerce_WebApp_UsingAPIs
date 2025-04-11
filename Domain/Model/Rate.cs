using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Rate
    {
        public int Id { get; set; }
        [Range(0, 5.0)]
        public double Value { get; set; }
        public string comment { get; set; }
        public DateTime DateTime { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ProductId { get; set; }

        public Product Product { get; set; }
    }

}

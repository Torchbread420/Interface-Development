using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public int UserId { get; set; }
        
        public User User { get; set; } = null!;

        public ICollection<Product> Products { get; } = new List<Product>();
    }
}

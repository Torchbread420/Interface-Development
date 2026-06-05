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

        public string? Name { get; set; }

        public TimeOnly OrderTime {  get; set; }

        public DateOnly OrderDate { get; set; }

        public required string OrderStatus { get; set; }

        public int UserId { get; set; }

        public required User User { get; set; }

        public required List<Product> Products { get; set; }

    }
}

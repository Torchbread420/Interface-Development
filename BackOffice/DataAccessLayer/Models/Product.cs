using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string SKU { get; set; }

        public string Category { get; set; } // nenamed Type > Category

        public string Location { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string ImagePath { get; set; }

        public decimal KostPrice { get; set; }
        [Required]
        public int Availability { get; set; } // renamed Stock > Availability
        [Required]
        public int MinimumAvailablility { get; set; } // renamed Min > MinimumAvailablility



        public decimal SalePercentage { get; set; }

        public string Status {  get; set; }

        public List<OrderProduct> OrderProducts { get; set; } = new();
        public ICollection<Order> Orders { get; } = new List<Order>();

        public Product()
        {
            SalePercentage = CalculateSalePercentage();
            Status = CalculateStatus();
        }
        private decimal CalculateSalePercentage()
        {
            if (Price == 0) return 0;
            return (Price - KostPrice) / Price * 100;
        }
        private string CalculateStatus()
        {
            string Status = string.Empty;
            if (Availability == 0)
            {
                Status = "Niet op voorraad";
            }
            else if (Availability < MinimumAvailablility) Status = "Laag voorraad";
            else Status = "Op voorraad";

            return Status;
        }
    }
}

using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.DataAccessLayer.Models
{
    public class Product
    {        
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string ImagePath { get; set; }

        public decimal KostPrice {  get; set; }

        public int Availability {  get; set; }

        public int MinimumAvailablility { get; set; }

        public string Type {  get; set; }

        public decimal SalePercentage { get; set; }

        public ICollection<Order> Orders { get; } = new List<Order>();

        public ICollection<Part> Parts { get; } = new List<Part>();

        public Product()
        {
            SalePercentage = CalculateSalePercentage();
        }
        private decimal CalculateSalePercentage()
        {
            if (Price == 0) return 0;
            return (Price - KostPrice) / Price * 100;
        }
    }
    
}

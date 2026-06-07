using System.ComponentModel.DataAnnotations;

namespace BackOffice.DataAccessLayer.Models
{
    public class BulkEdit
    {
        public decimal KostPrice { get; set; }
        public decimal SalePercentage { get; set; }
        [Required]
        public List<int> ProductIds { get; set; } = [];

        public BulkEdit() { }
        public BulkEdit(List<int> productIds)
        {
            ProductIds = productIds;
        }
    }
}

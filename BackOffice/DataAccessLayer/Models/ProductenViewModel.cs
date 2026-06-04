using System.ComponentModel.DataAnnotations;

namespace BackOffice.DataAccessLayer.Models
{
    public class ProductenViewModel
    {
        [Required]
        public List<Product> Producten { get; set; }
        public Product? ProductEditForm { get; set; }
        public BulkEdit? BulkEdit { get; set; }

        public ProductenViewModel() { }

        public ProductenViewModel(List<Product> producten, Product? productEditForm = null, BulkEdit? bulkEdit = null)
        {
            Producten = producten;
            ProductEditForm = productEditForm;
            BulkEdit = bulkEdit;
        }
    }

}

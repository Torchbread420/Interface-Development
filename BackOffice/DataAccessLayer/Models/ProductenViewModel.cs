namespace BackOffice.DataAccessLayer.Models
{
    public class ProductenViewModel
    {
        public List<Product> Producten { get; set; } = [];
        public Product? ProductEditForm { get; set; }
        public BulkEdit? BulkEdit { get; set; } = new();
    }
}

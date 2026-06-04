namespace BackOffice.Models
{
    public class ProductInventoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string SKU { get; set; } = "";
        public string Category { get; set; } = "";
        public string Location { get; set; } = "";
        public int Stock { get; set; }
        public int Min { get; set; }
        public decimal Price { get; set; }
        public string Status => Stock == 0 ? "Niet op voorraad" : (Stock < Min ? "Laag voorraad" : "Op voorraad");
    }
}
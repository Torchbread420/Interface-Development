namespace BackOffice.Models
{
    public class OrderViewModel
    {
        public List<OrderWithTotal> Orders { get; set; } = new();

    }

    public class OrderWithTotal
    {
        public Order Order { get; set; }
        public decimal TotalPrice { get; set; }
    }
}

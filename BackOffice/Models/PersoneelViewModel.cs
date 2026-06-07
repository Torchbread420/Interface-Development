using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Models;

namespace BackOffice.Models
{
    public class WeekHours
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        // Ma, Di, Wo, Do, Vr
        public int[] Days { get; set; } = new int[5];
        public int Total => Days?.Sum() ?? 0;
    }

    public class OrderListItem
    {
        public int OrderId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string Status { get; set; } = "In behandeling";
        public decimal Amount { get; set; }
    }

    public class PersoneelViewModel
    {
        public IEnumerable<User> Users { get; set; } = Enumerable.Empty<User>();
        public IEnumerable<OrderListItem> OrdersList { get; set; } = Enumerable.Empty<OrderListItem>();
        public IEnumerable<WeekHours> WeekHours { get; set; } = Enumerable.Empty<WeekHours>();
    }
}
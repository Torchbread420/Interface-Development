using System.Collections.Generic;
using System.Linq;

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

    public class PersoneelViewModel
    {
        public IEnumerable<User> Users { get; set; } = Enumerable.Empty<User>();
        public IEnumerable<OrderWithTotal> Orders { get; set; } = Enumerable.Empty<OrderWithTotal>();
        public IEnumerable<WeekHours> WeekHours { get; set; } = Enumerable.Empty<WeekHours>();
    }
}
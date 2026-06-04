using System.Collections.Generic;

namespace BackOffice.Models
{
    public class PaginatedProductViewModel
    {
        public List<ProductInventoryViewModel> Items { get; set; } = new();
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int) System.Math.Ceiling(TotalCount / (double) PageSize);

        // overall counts (over full result set, not just the page)
        public int OnStockCount { get; set; }
        public int LowCount { get; set; }
        public int OutCount { get; set; }
    }
}
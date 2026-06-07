
using BackOffice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace BackOffice.DataAccessLayer.Models
    {
    public class PaginatedProductViewModel
    {
        public List<Product> Items { get; set; } = new();
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)System.Math.Ceiling(TotalCount / (double)PageSize);

        // overall counts (over full result set, not just the page)
        public int OnStockCount { get; set; }
        public int LowCount { get; set; }
        public int OutCount { get; set; }
        public Product? ProductEditForm { get; set; }
        public BulkEdit? BulkEdit { get; set; }

        public PaginatedProductViewModel() { }

        public PaginatedProductViewModel(List<Product> items, Product? productEditForm = null, BulkEdit? bulkEdit = null)
        {
            Items = items;
            ProductEditForm = productEditForm;
            BulkEdit = bulkEdit;
            SetTotal();
            SetPage();
            SetStockAmmount();
        }
            
        private void SetTotal()
        {
            TotalCount = Items.Count;
        }
        private void SetPage()
        {
            PageSize = 10;
            Page = 1;
        }
        private void SetStockAmmount()
        {
            var onStockCount = Items.Count(x => x.Availability > 0 && x.Availability > x.MinimumAvailablility);
            var lowCount = Items.Count(x => x.Availability > 0 && x.Availability < x.MinimumAvailablility);
            var outCount = Items.Count(x => x.Availability <= 0);

            OnStockCount = onStockCount;
            LowCount = lowCount;
            OutCount = outCount;
        }
    }
}

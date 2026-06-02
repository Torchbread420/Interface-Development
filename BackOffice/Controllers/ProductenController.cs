using BackOffice.DataAccessLayer.Models;
using BackOffice.Models;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BackOffice.Controllers
{
    public class ProductenController(ILogger<ProductenController> logger, ProductRepository productService) : Controller
    {
        private readonly ILogger<ProductenController> _logger = logger;
        private readonly ProductRepository _productService = productService;
        public List<Product>? Producten;
        public BulkEdit? BulkEdit = new();
        public Product? ProductEditForm = new Product();



        // Edit form for a product
        // TEMP: should add a edit form before editing a product
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null) return NotFound();
            return PartialView("_EditProduct", product);
        }

        // Save edited product
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (!ModelState.IsValid) return PartialView("_EditProduct", product);
                
            _productService.UpdateProduct(product);
            return RedirectToAction(nameof(Index));
        }

        // Remove a product
        [HttpPost]
        public IActionResult Remove(int id)
        {
            _productService.DeleteProductById(id);
            return RedirectToAction(nameof(Index));
        }

        // Handle bulk checkbox selection
        // TEMP: should add a edit step or a confirmation step before deleting or editing multiple products
        [HttpPost]
        public IActionResult BulkAction(List<int> selectedIds, string action)
        {
            if (selectedIds == null || !selectedIds.Any())
                return RedirectToAction(nameof(Index));

            if (action == "delete") _productService.DeleteProductsById(selectedIds);
            if (action == "edit") _productService.UpdateProductsById(selectedIds);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult GetProduct(int id)
        {
            var product = _productService.GetProductById(id);

            if (product == null)
                return NotFound();

            return Json(product);
        }

        [HttpPost]
        public IActionResult SaveProduct(Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _productService.UpdateProduct(product);

            return Ok();
        }

        // renders the list view on load
        public IActionResult Index()
        {
            Producten = _productService.GetAllProducts().ToList();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

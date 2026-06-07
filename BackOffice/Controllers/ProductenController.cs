using BackOffice.DataAccessLayer.Models;
using BackOffice.Models;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BackOffice.Controllers
{
    public class ProductenController(ILogger<ProductenController> logger, IProductRepository productService) : Controller
    {
        private readonly ILogger<ProductenController> _logger = logger;
        private readonly IProductRepository _productService = productService;



        // Edit form for a product
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null) return NotFound();
            PaginatedProductViewModel productenViewModel = new(productService.GetAllProducts().ToList(), product, null);
            return PartialView("_EditProduct", productenViewModel);
        }

        // save edit
        [HttpPost]
        public IActionResult EditProduct(PaginatedProductViewModel viewModel)
        {
            Product? product = viewModel.ProductEditForm;
            if (product == null) return NotFound();
            _productService.UpdateProduct(product);
            return RedirectToAction("Home", "Producten");
        }

        // Remove a product
        [HttpPost]
        public IActionResult Remove(int id)
        {
            _productService.DeleteProductById(id);
            return RedirectToAction("Home", "Producten");
        }

        // Handle bulk checkbox selection
        // TEMP: should add a edit step or a confirmation step before deleting or editing multiple products
        [HttpPost]
        public IActionResult BulkAction(List<int> selectedIds, string action)
        {
            if (selectedIds == null || selectedIds.Count == 0) return RedirectToAction("Home", "Producten");

            if (action == "delete")
            {
                _productService.DeleteProductsById(selectedIds);
                return RedirectToAction("Home", "Producten");
            }
            BulkEdit bulkEdit = new(selectedIds);
            PaginatedProductViewModel productenViewModel = new(productService.GetAllProducts().ToList(), null, bulkEdit);
            return PartialView("_BulkEditProducten", productenViewModel);
        }

        [HttpPost]
        public IActionResult BulkEditSave(BulkEdit? bulkEdit)
        {
            if (bulkEdit == null) return NotFound();
            _productService.EditProducts(bulkEdit);
            return RedirectToAction("Home", "Producten");
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
            PaginatedProductViewModel vm = new(_productService.GetAllProducts().ToList(), null, null);

            return View(vm);
        }

        public IActionResult Privacy()
        {
            PaginatedProductViewModel vm = new(_productService.GetAllProducts().ToList(), null, null);
            return View(vm);
        }

        public IActionResult Producten()
        {
            PaginatedProductViewModel vm = new(_productService.GetAllProducts().ToList(), null, null);
            return View(vm);
        }
        public IActionResult Categories()
        {
            var products = _productService.GetAllProducts().ToList();
            return View(products);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

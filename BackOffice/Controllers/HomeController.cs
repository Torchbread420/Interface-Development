using BackOffice.DataAccessLayer.Models;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BackOffice.Controllers
{
    public class HomeController(ILogger<HomeController> logger, IProductRepository productService) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;
        private readonly IProductRepository _productService = productService;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Producten()
        {
            ProductenViewModel vm = new([.. _productService.GetAllProducts()]);
            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

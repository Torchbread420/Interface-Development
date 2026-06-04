using BackOffice.Models;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace BackOffice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;

        public HomeController(
            ILogger<HomeController> logger,
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            IUserRepository userRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            var orders = _orderRepository.GetAllOrders().ToList();
            var products = _productRepository.GetAllProducts().ToList();
            var users = _userRepository.GetAllUsers().ToList();

            ViewBag.TotalProducts = products.Count;
            ViewBag.TotalOrders = orders.Count;
            ViewBag.TotalUsers = users.Count;
            ViewBag.RecentOrders = orders;
            ViewBag.Products = products;

            return View();
        }

        public IActionResult Personeel()
        {
            return View();
        }

        public IActionResult Order()
        {
            return View();
        }

        public IActionResult Product()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CheckLogin(LoginViewModel model)
        {
            bool result = _userRepository.UserExists(model.Username, model.Password);
            if (!result) {
                ViewBag.ErrorMessage = "Incorrect username or password.";
                return View("Login");
            }

            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
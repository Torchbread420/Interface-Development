using BackOffice.Models;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;


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

        // Product list: uses real repository, supports optional search & paging
        public IActionResult Product(string? q, int page = 1, int pageSize = 10)
        {
            var allProducts = _productRepository.GetAllProducts().ToList();

            // Map to inventory view model (enrich with deterministic inventory data for display)
            var mapped = allProducts.Select(p =>
            {
                // SKU: initials + id
                var initials = string.Concat(p.Name.Split(' ', System.StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => char.ToUpperInvariant(s[0])));
                var sku = $"{initials}-{p.Id}";

                // Category: use first Part name or fallback
                var category = p.Parts?.FirstOrDefault()?.Name ?? "Algemeen";

                // Location deterministic: Rek A..F with number
                var rackLetter = (char)('A' + (p.Id % 6));
                var rackNumber = (p.Id % 10) + 1;
                var location = $"Rek {rackLetter}{rackNumber}";

                // Deterministic stock/min values for UI (replace with real fields when available)
                var stock = System.Math.Abs(p.Name.GetHashCode()) % 6000; // deterministic-ish
                var min = (p.Id * 37) % 200 + 10;

                return new ProductInventoryViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    SKU = sku,
                    Category = category,
                    Location = location,
                    Stock = stock,
                    Min = min,
                    Price = p.Price
                };
            }).ToList();

            // Apply search if provided
            if (!string.IsNullOrWhiteSpace(q))
            {
                var sq = q.Trim().ToLowerInvariant();
                mapped = mapped.Where(x =>
                    x.Name.ToLowerInvariant().Contains(sq) ||
                    x.SKU.ToLowerInvariant().Contains(sq) ||
                    x.Category.ToLowerInvariant().Contains(sq)
                ).ToList();
            }

            // counts over the result set
            var onStockCount = mapped.Count(x => x.Stock > 0);
            var lowCount = mapped.Count(x => x.Stock > 0 && x.Stock < x.Min);
            var outCount = mapped.Count(x => x.Stock == 0);

            var total = mapped.Count;
            var items = mapped.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var vm = new PaginatedProductViewModel
            {
                Items = items,
                TotalCount = total,
                Page = page,
                PageSize = pageSize,
                OnStockCount = onStockCount,
                LowCount = lowCount,
                OutCount = outCount
            };

            return View(vm);
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
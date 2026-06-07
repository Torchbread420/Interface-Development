using BackOffice.DataAccessLayer.Models;
using BackOffice.Models;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq; // added for LINQ helpers
using System.Globalization; // for parsing helpers


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
            var users = _userRepository.GetAllUsers().ToList();
            var orders = _orderRepository.GetAllOrders().ToList();

            // Build week hours from persisted WorkSchedules for all users
            var weekHours = new List<WeekHours>();

            foreach (var u in users)
            {
                var w = new WeekHours
                {
                    UserId = u.Id,
                    Name = u.Name,
                    Days = new int[5] // Ma..Vr
                };

                // fill days from WorkSchedules if present
                if (u.WorkSchedules != null && u.WorkSchedules.Any())
                {
                    foreach (var s in u.WorkSchedules)
                    {
                        if (s.DayIndex >= 0 && s.DayIndex < 5)
                        {
                            w.Days[s.DayIndex] = s.Hours;
                        }
                    }
                }
                else
                {
                    // default 40u for new users without schedule
                    w.Days = new[] { 8, 8, 8, 8, 8 };
                }

                weekHours.Add(w);
            }

            var vm = new PersoneelViewModel
            {
                Users = users,
                WeekHours = weekHours,
                Orders = orders.Select(o => new OrderWithTotal
                {
                    Order = o,
                    TotalPrice = o.OrderProducts.Sum(op => op.Quantity * op.Product.Price)
                }).ToList()
            };

            return View(vm);
        }

        public IActionResult Order()
        {
            return View();
        }

        public IActionResult Producten()
        {
            PaginatedProductViewModel vm = new(_productRepository.GetAllProducts().ToList(), null, null);
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

        // GET: show empty form
        public IActionResult CreateEmployee()
        {
            return View(new CreateUserViewModel());
        }

        // POST: create and save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEmployee(CreateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            DateOnly dob;
            try
            {
                dob = DateOnly.ParseExact(model.DateOfBirth, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch
            {
                ModelState.AddModelError(nameof(model.DateOfBirth), "Ongeldige datum");
                return View(model);
            }

            var digits = new string((model.PhoneNumber ?? string.Empty).Where(char.IsDigit).ToArray());
            int phone = 0;
            if (!string.IsNullOrEmpty(digits) && digits.Length <= 9)
            {
                int.TryParse(digits, out phone);
            }
            else if (!string.IsNullOrEmpty(digits))
            {
                var last = digits.Length > 9 ? digits[^9..] : digits;
                int.TryParse(last, out phone);
            }

            var user = new User
            {
                Name = model.Name,
                Password = model.Password,
                Email = model.Email,
                Address = model.Address ?? string.Empty,
                DateOfBirth = dob,
                PhoneNumber = phone,
                UserType = model.UserType
            };

            // create schedule entries (Ma..Vr)
            user.WorkSchedules.Add(new BackOffice.Models.WorkSchedule { DayIndex = 0, Hours = model.Ma });
            user.WorkSchedules.Add(new BackOffice.Models.WorkSchedule { DayIndex = 1, Hours = model.Di });
            user.WorkSchedules.Add(new BackOffice.Models.WorkSchedule { DayIndex = 2, Hours = model.Wo });
            user.WorkSchedules.Add(new BackOffice.Models.WorkSchedule { DayIndex = 3, Hours = model.Do });
            user.WorkSchedules.Add(new BackOffice.Models.WorkSchedule { DayIndex = 4, Hours = model.Vr });

            _userRepository.AddUser(user);

            return RedirectToAction("Personeel");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
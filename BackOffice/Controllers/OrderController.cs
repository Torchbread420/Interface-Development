using BackOffice.Models;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BackOffice.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public IActionResult Index()
        {
            var orders = _orderRepository.GetAllOrders().ToList();

            var viewModel = new OrderViewModel
            {
                Orders = orders.Select(o => new OrderWithTotal
                {
                    Order = o,
                    TotalPrice = o.Products.Sum(p => p.Price)
                }).ToList()
            };

            return View(viewModel);
        }
        public IActionResult OrderDetails()
        {
            return View();
        }
    }
}

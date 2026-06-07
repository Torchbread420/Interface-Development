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
                    TotalPrice = o.OrderProducts.Sum(op => op.Quantity * op.Product.Price)
                }).ToList()
            };

            return View(viewModel);
        }
        public IActionResult OrderDetails(int id)
        {
            var order = _orderRepository.GetOrderById(id);

            if (order == null)
                return NotFound();

            var viewModel = new OrderDetailsViewModel
            {
                Order = order,
                TotalPrice = order.OrderProducts?.Sum(op => op.Quantity * op.Product.Price) ?? 0
            };

            return View(viewModel);
        }
    }
}

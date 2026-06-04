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
            var orderviewmodel = new OrderViewModel
            {
                Orders = _orderRepository.GetAllOrders().ToList()
            };
            return View(orderviewmodel);
        }
    }
}

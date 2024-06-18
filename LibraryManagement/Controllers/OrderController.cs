using LibraryManagement.Service;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;


        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;

        }
        public IActionResult OrderSuccess()
        {
            return View();
        }
    }
}

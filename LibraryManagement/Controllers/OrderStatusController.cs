using LibraryManagement.Models;
using LibraryManagement.Service;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class OrderStatusController : Controller
    {
        private readonly IOrderStatusService service;

        public OrderStatusController(IOrderStatusService service)
        {
            this.service = service;
        }
        // GET: OrderStatusController
        public ActionResult Index()
        {
            var model = service.GetAllOrderStatus();
            return View(model);
        }

        // GET: OrderStatusController/Details/5
        public ActionResult Details(int id)
        {
            var stats = service.GetOrderStatusById(id);
            return View(stats);
        }

        // GET: OrderStatusController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderStatusController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderStatus orderstatus)
        {
            try
            {
                int result = service.AddOrderStatus(orderstatus);
                if (result >= 1)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.ErrorMsg = "Something went wrong";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // GET: OrderStatusController/Edit/5
        public ActionResult Edit(int id)
        {
            var stats = service.GetOrderStatusById(id);
            return View(stats);
        }

        // POST: OrderStatusController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, OrderStatus orderstatus)
        {
            try
            {
                int result = service.EditOrderStatus(orderstatus);
                if (result >= 1)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.ErrorMsg = "Something went wrong";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // GET: OrderStatusController/Delete/5
        public ActionResult Delete(int id)
        {
            var stats = service.GetOrderStatusById(id);
            return View(stats);
        }

        // POST: OrderStatusController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                int result = service.DeleteOrderStatus(id);
                if (result >= 1)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.ErrorMsg = "Something went wrong";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }
    }
}

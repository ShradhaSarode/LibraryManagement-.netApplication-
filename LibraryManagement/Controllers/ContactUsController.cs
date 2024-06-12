using LibraryManagement.Models;
using LibraryManagement.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class ContactUsController : Controller
    {
        public readonly IContactUsService service;
        public ContactUsController(IContactUsService service)
        {
            this.service = service;
        }
        // GET: ContactUsController
        public ActionResult Index()
        {
            var model = service.GetAllMessages();
            return View(model);
        }

        // GET: ContactUsController/Details/5
        public ActionResult Details(int id)
        {
            var contactus = service.GetMessageById(id);
            return View(contactus);
        }

        // GET: ContactUsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactUsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContactUs contact)
        {
            try
            {
                int result = service.AddMessage(contact);
                if (result >= 1)
                {
                    return RedirectToAction(nameof(ThankYou));
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



        // GET: ContactUsController/Delete/5
        public ActionResult Delete(int id)
        {
            var contactus = service.DeleteMessage(id);
            return View(contactus);
        }

        // POST: ContactUsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                int result = service.DeleteMessage(id);
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

        public ActionResult ThankYou()
        {
            return View();
        }
    }
}

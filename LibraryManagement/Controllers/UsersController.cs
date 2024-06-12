using LibraryManagement.Data;
using LibraryManagement.Models;
using LibraryManagement.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace LibraryManagement.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationDbContext db;
        public readonly IUsersService service;
        public UsersController(IUsersService service)
        {
            this.service = service;
        }
        // GET: UsersController
        public ActionResult Index()
        {
            var model = service.GetUsers();
            return View(model);
        }

        // GET: UsersController/Details/5
        public ActionResult Details(int id)
        {
            var user = service.GetUserById(id);
            return View(user);
        }

        // GET: UsersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Users user)
        {
            try
            {
                int result = service.AddUsers(user);
                if (result >= 1)
                {
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    ViewBag.ErrorMsg = "Something went wrong";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View();
            }
        }

        // GET: UsersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsersController/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users login)
        {
            try
            {
                var user = service.GetUserByEmail(login.Email);

                if (user != null && user.Password == login.Password)
                {
                    if (user.IsActive != 1) // Assuming 1 means active and 0 means inactive
                    {
                        ViewBag.ErrorMsg = "Your account is inactive. Please contact admin.";
                        return View(login);
                    }

                    HttpContext.Session.SetString("UserEmail", user.Email);
                    HttpContext.Session.SetInt32("UserRole", user.RoleID);
                    HttpContext.Session.SetInt32("UserId", user.UserID);
                    // Check the role of the user
                    if (user.RoleID == 1) // Assuming 1 is the role ID for admin
                    {
                        return RedirectToAction("Index", "Book");
                    }
                    else if (user.RoleID == 2) // Assuming 2 is the role ID for customer
                    {
                        return RedirectToAction("BookList", "Book");
                    }
                }
                ViewBag.ErrorMsg = "Invalid email or password";

                return View(login);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(login);
            }
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult UpdateStatus(int userId, int isActive)
        {
            service.UpdateUserStatus(userId, isActive);
            return RedirectToAction("Index");
        }
    }
}

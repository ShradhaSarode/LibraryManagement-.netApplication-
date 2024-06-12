using LibraryManagement.Models;
using LibraryManagement.Service;
using LibraryManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class BookController : Controller
    {
        public readonly IBookService service;
        private readonly ICategoryService categoryService;
        public readonly IAuthorService aservice;

        private Microsoft.AspNetCore.Hosting.IHostingEnvironment env;
        public BookController(IBookService service, 
                              Microsoft.AspNetCore.Hosting.IHostingEnvironment e,
                              ICategoryService categoryService,
                              IAuthorService aservice)
        {
            this.service = service;
            this.categoryService = categoryService;
            this.aservice = aservice;
            env = e;
        }
        // GET: BookController
        public ActionResult Index()
        {
            var model = service.GetBook();
            return View(model);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            //var b = service.GetBookById(id);
            //return View(b);
            var b = service.GetBookById(id);
            if (b == null)
            {
                return NotFound();
            }
            return View(b);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            var model = categoryService.GetCategories();
            ViewBag.Categories = model;

            var a = aservice.GetAuthor();
            ViewBag.Authors = a;

            return View();
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book b, IFormFile file)
        {
            try
            {
                using (var fs = new FileStream(env.WebRootPath + "\\images\\" + file.FileName, FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(fs);
                }

                b.CoverImage = "~/images/" + file.FileName;
                int result = service.AddBook(b);
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

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = service.GetBookById(id);

            var categories = categoryService.GetCategories();
            ViewBag.Categories = categories;

            var a = aservice.GetAuthor();
            ViewBag.Authors = a;
           
            HttpContext.Session.SetString("oldCoverImage", book.CoverImage);
            return View(book);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Book b, IFormFile file)
        {
            try
            {
                string oldCoverImage = HttpContext.Session.GetString("oldCoverImage");
                if (file != null)
                {
                    using (var fs = new FileStream(env.WebRootPath + "\\images\\" + file.FileName, FileMode.Create, FileAccess.Write))
                    {
                        file.CopyTo(fs);
                    }
                    b.CoverImage = "~/images/" + file.FileName;

                    string[] str = oldCoverImage.Split("/");
                    string str1 = (str[str.Length - 1]);
                    string path = env.WebRootPath + "\\images\\" + str1;
                    System.IO.File.Delete(path);
                }
                else
                {
                    b.CoverImage = oldCoverImage;
                }

                int result = service.EditBook(b);
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

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            var emp = service.GetBookById(id);
            return View(emp);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                int result = service.DeleteBook(id);
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

        public ActionResult BookList(string searchTerm)
        {
            var model = string.IsNullOrEmpty(searchTerm) ? service.GetBook() : service.GetBookByName(searchTerm);
            ViewBag.SearchTerm = searchTerm;
            return View(model);
        }
    }
}

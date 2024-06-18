using LibraryManagement.Data;
using LibraryManagement.Models;

namespace LibraryManagement.Repositories
{
    public class BookRepo : IBookRepo
    {
        private readonly ApplicationDbContext db;
        public BookRepo(ApplicationDbContext d)
        {
            this.db = d;
        }
        public int AddBook(Book b)
        {
            
            int result = 0;
            db.Books.Add(b);
            result = db.SaveChanges();
            return result;
        }

        public int DeleteBook(int id)
        {
            int result = 0;
            var model = db.Books.Where(b => b.BookID == id).FirstOrDefault();
            if (model != null)
            {
                db.Books.Remove(model);
                result = db.SaveChanges();
            }
            return result;
        }

        public int EditBook(Book b)
        {
            int result = 0;
            var model = db.Books.Where(bk => bk.BookID == b.BookID).FirstOrDefault();
            if (model != null)
            {
                model.Title = b.Title;
                model.AuthorID = b.AuthorID;
                model.PublishedYear = b.PublishedYear;
                model.CoverImage = b.CoverImage;
                model.Price = b.Price;
                model.CategoryId = b.CategoryId;
                model.Stock = b.Stock;
                //model.Author = b.Author;

                result = db.SaveChanges();
            }
            return result;
        }

        public Book GetBookById(int id)
        {
            return db.Books.Where(b => b.BookID == id).SingleOrDefault();
            //var res = (from b in db.Books
            //            where b.BookID == id 
            //            select b).FirstOrDefault();
            //return res;

        }

        public IEnumerable<Book> GetBookByName(string name)
        {
            var model = from b in db.Books
                        where b.Title.Contains(name)
                        select b;
            return model;
        }

        public IEnumerable<Book> GetBooks()
        {
            var model = (from b in db.Books
                         join a in db.Authors on b.AuthorID equals a.AuthorID
                         join c in db.Categories on b.CategoryId equals c.CategoryId
                         select new Book
                         {
                             BookID=b.BookID,
                             Title = b.Title,
                             AuthorID = b.AuthorID,
                             Name=a.Name,
                             PublishedYear = b.PublishedYear,
                             CoverImage = b.CoverImage,
                             Price = b.Price,
                             CategoryId = b.CategoryId,
                             Categoryname = c.CategoryName,
                             Stock = b.Stock
                         }).ToList();
            return model;

        }
    }
}

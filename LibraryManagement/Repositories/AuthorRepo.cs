using LibraryManagement.Data;
using LibraryManagement.Models;

namespace LibraryManagement.Repositories
{
    public class AuthorRepo : IAuthorRepo
    {
        private readonly ApplicationDbContext db;
        public AuthorRepo(ApplicationDbContext db)
        {
            this.db = db;
        }
        public int AddAuthor(Author b)
        {
            int result = 0;
            db.Authors.Add(b);
            result = db.SaveChanges();
            return result;
        }

        public int DeleteAuthor(int id)
        {
            int result = 0;
            var model = db.Authors.Where(b => b.AuthorID == id).FirstOrDefault();
            if (model != null)
            {
                db.Authors.Remove(model);
                result = db.SaveChanges();
            }
            return result;
        }

        public int EditAuthor(Author b)
        {
            int result = 0;
            var model = db.Authors.Where(bk => bk.AuthorID == b.AuthorID).FirstOrDefault();
            if (model != null)
            {
                model.Name = b.Name;
                result = db.SaveChanges();
            }
            return result;
        }

        public IEnumerable<Author> GetAuthor()
        {
            var model = (from u in
                             db.Authors
                         select u).ToList();
            return model;
        }

        public Author GetAuthorById(int id)
        {
            return db.Authors.Where(b => b.AuthorID == id).SingleOrDefault();
        }
    }
}

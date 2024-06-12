
using LibraryManagement.Models;
using LibraryManagement.Data;

namespace LibraryManagement.Repositories
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly ApplicationDbContext db;

        public CategoryRepo(ApplicationDbContext db)
        {
            this.db = db;
        }
        public int AddCategory(Category category)
        {
            
            int result = 0;
            db.Categories.Add(category);
            result = db.SaveChanges();
            return result;
        }

        public int DeleteCategory(int id)
        {
            int result = 0;
            var model = db.Categories.Where(category => category.CategoryId == id).FirstOrDefault();
            if (model != null)
            {
                db.Categories.Remove(model);
                result = db.SaveChanges();
            }
            return result;
        }

        public int EditCategory(Category category)
        {
            int result = 0;
            var model = db.Categories.Where(catgry => catgry.CategoryId == category.CategoryId).FirstOrDefault();
            if (model != null)
            {
                model.CategoryName = category.CategoryName;
                 result = db.SaveChanges();
            }
            return result;
        }

        public IEnumerable<Category> GetCategories()
        {
            var model = (from category in db.Categories
                         select category).ToList();
            return model;
        }

        public Category GetCategoryById(int id)
        {
            return db.Categories.Where(x => x.CategoryId == id).SingleOrDefault();
        }
    }
}

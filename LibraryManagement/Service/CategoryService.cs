using LibraryManagement.Models;
using LibraryManagement.Repositories;

namespace LibraryManagement.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepo repo;

        public CategoryService(ICategoryRepo repo)
        {
            this.repo = repo;
        }
        public int AddCategory(Category category)
        {
           return repo.AddCategory(category);
        }

        public int DeleteCategory(int id)
        {
            return repo.DeleteCategory(id);
        }

        public int EditCategory(Category category)
        {
            return repo.EditCategory(category);
        }

        public IEnumerable<Category> GetCategories()
        {
            return repo.GetCategories();
        }

        public Category GetCategoryById(int id)
        {
            return repo.GetCategoryById(id);
        }
    }
}

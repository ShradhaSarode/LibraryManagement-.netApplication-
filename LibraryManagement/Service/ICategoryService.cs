using LibraryManagement.Models;

namespace LibraryManagement.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();
        Category GetCategoryById(int id);
        int AddCategory(Category category);
        int EditCategory(Category category);
        int DeleteCategory(int id);
    }
}

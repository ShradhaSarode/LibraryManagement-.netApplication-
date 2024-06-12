using LibraryManagement.Models;

namespace LibraryManagement.Repositories
{
    public interface ICategoryRepo
    {
        IEnumerable<Category> GetCategories();
        Category GetCategoryById(int id);
        int AddCategory(Category category);
        int EditCategory(Category category);
        int DeleteCategory(int id);
    }
}

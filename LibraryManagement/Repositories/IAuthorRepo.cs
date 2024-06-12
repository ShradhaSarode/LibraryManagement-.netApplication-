using LibraryManagement.Models;

namespace LibraryManagement.Repositories
{
    public interface IAuthorRepo
    {
        IEnumerable<Author> GetAuthor();
        Author GetAuthorById(int id);
        int AddAuthor(Author b);
        int EditAuthor(Author b);
        int DeleteAuthor(int id);
    }
}

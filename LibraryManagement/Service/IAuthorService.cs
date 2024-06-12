using LibraryManagement.Models;

namespace LibraryManagement.Service
{
    public interface IAuthorService
    {
        IEnumerable<Author> GetAuthor();
        Author GetAuthorById(int id);
        int AddAuthor(Author b);
        int EditAuthor(Author b);
        int DeleteAuthor(int id);
    }
}

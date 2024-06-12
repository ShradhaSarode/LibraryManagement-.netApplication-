using LibraryManagement.Models;

namespace LibraryManagement.Repositories
{
    public interface IBookRepo
    {
        IEnumerable<Book> GetBooks();
        Book GetBookById(int id);
        int AddBook(Book b);
        int EditBook(Book b);
        int DeleteBook(int id);
        IEnumerable<Book> GetBookByName(string name);
    }
}

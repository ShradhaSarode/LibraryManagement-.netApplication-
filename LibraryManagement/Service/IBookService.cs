using LibraryManagement.Models;

namespace LibraryManagement.Service
{
    public interface IBookService
    {
        IEnumerable<Book> GetBook();
        Book GetBookById(int id);
        int AddBook(Book b);
        int EditBook(Book b);
        int DeleteBook(int id);
        IEnumerable<Book> GetBookByName(string name);
    }
}

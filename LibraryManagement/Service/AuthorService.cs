using LibraryManagement.Models;
using LibraryManagement.Repositories;

namespace LibraryManagement.Service
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepo repo;
        public AuthorService(IAuthorRepo repo)
        {
            this.repo = repo;
        }
        public int AddAuthor(Author b)
        {
            return repo.AddAuthor(b);
        }

        public int DeleteAuthor(int id)
        {
            return repo.DeleteAuthor(id);
        }

        public int EditAuthor(Author b)
        {
            return repo.EditAuthor(b);
        }

        public IEnumerable<Author> GetAuthor()
        {
            return repo.GetAuthor();
        }

        public Author GetAuthorById(int id)
        {
            return repo.GetAuthorById(id);
        }
    }
}

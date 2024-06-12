using LibraryManagement.Models;
using LibraryManagement.Repositories;

namespace LibraryManagement.Service
{
    public class ContactUsService : IContactUsService
    {
        private readonly IContactUsRepo repo;

        public ContactUsService(IContactUsRepo repo)
        {
            this.repo = repo;
        }
        public int AddMessage(ContactUs contactus)
        {
            return repo.AddMessage(contactus);
        }

        public int DeleteMessage(int id)
        {
            return repo.DeleteMessage(id);
        }

        public IEnumerable<ContactUs> GetAllMessages()
        {
            return repo.GetAllMessages();
        }

        public ContactUs GetMessageById(int id)
        {
            return repo.GetMessageById(id);
        }
    }
}

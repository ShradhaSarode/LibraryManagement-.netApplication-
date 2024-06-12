using LibraryManagement.Models;

namespace LibraryManagement.Service
{
    public interface IContactUsService
    {
        IEnumerable<ContactUs> GetAllMessages();
        ContactUs GetMessageById(int id);
        int AddMessage(ContactUs contactus);
        int DeleteMessage(int id);
    }
}

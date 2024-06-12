using LibraryManagement.Models;

namespace LibraryManagement.Repositories
{
    public interface IOrderRepo
    {
        IEnumerable<Orders> GetOrders(int userId);
    }
}

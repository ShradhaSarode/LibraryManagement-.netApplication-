using LibraryManagement.Models;

namespace LibraryManagement.Service
{
    public interface IOrderService
    {
        IEnumerable<Orders> GetOrders(int userId);
    }
}

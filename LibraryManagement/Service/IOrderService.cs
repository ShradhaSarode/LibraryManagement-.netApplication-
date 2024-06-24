using LibraryManagement.Models;

namespace LibraryManagement.Service
{
    public interface IOrderService
    {
        IEnumerable<Orders> GetOrders(int userId);
        IEnumerable<Orders> GetAllOrders();
        int UpdateOrderStatus(int orderItemId, int orderStatusId);
    }
}

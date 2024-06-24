using LibraryManagement.Models;
using LibraryManagement.Repositories;

namespace LibraryManagement.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo repo;

        public OrderService(IOrderRepo repo)
        {
            this.repo = repo;
        }

        public IEnumerable<Orders> GetAllOrders()
        {
            return repo.GetAllOrders();
        }

        public IEnumerable<Orders> GetOrders(int userId)
        {
            return repo.GetOrders(userId);
        }

        public int UpdateOrderStatus(int orderItemId, int orderStatusId)
        {
            return repo.UpdateOrderStatus(orderItemId, orderStatusId);
        }
    }
}

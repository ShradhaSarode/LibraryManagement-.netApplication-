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
        public IEnumerable<Orders> GetOrders(int userId)
        {
            return repo.GetOrders(userId);
        }
    }
}

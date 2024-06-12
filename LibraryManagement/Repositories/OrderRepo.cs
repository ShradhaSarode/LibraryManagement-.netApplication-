using LibraryManagement.Data;
using LibraryManagement.Models;

namespace LibraryManagement.Repositories
{
    public class OrderRepo : IOrderRepo
    {
        private readonly ApplicationDbContext db;

        public OrderRepo(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<Orders> GetOrders(int userId)
        {
            var orders = from order in db.Orders
                         where order.UserId == userId
                         join orderitem in db.OrderItems on order.OrderId equals orderitem.OrderId
                         join orderstatus in db.OrderStatus on orderitem.OrderStatusId equals orderstatus.OrderStatusId
                         select new Orders
                         {
                             OrderId = order.OrderId,
                             UserId = order.UserId,
                             OrderDate = order.OrderDate,
                             TotalAmount = order.TotalAmount,
                             OrderItems = (from item in db.OrderItems
                                           where item.OrderId == order.OrderId
                                           join status in db.OrderStatus on item.OrderStatusId equals status.OrderStatusId
                                           select new OrderItems
                                           {
                                               OrderItemID = item.OrderItemID,
                                               OrderId = item.OrderId,
                                               ProductId = item.ProductId,
                                               OrderStatusId = item.OrderStatusId,
                                               Quantity = item.Quantity,
                                               Price = item.Price,
                                               OrderStatus = new OrderStatus
                                               {
                                                   OrderStatusId = status.OrderStatusId,
                                                   Status = status.Status
                                               }
                                           }).ToList()
                         };

            return orders.ToList();
        }
    }
}

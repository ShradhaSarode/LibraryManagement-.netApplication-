using LibraryManagement.Data;
using LibraryManagement.Models;

namespace LibraryManagement.Repositories
{
    public class OrderStatusRepo : IOrderStatusRepo
    {
        private readonly ApplicationDbContext db;

        public OrderStatusRepo(ApplicationDbContext db)
        {
            this.db = db;
        }

        public int AddOrderStatus(OrderStatus orderStatus)
        {
            int result = 0;
            db.OrderStatus.Add(orderStatus);
            result = db.SaveChanges();
            return result;
        }

        public int DeleteOrderStatus(int id)
        {
            int result = 0;
            var model = db.OrderStatus.Where(ord => ord.OrderStatusId == id).FirstOrDefault();
            if (model != null)
            {
                db.OrderStatus.Remove(model);
                result = db.SaveChanges();
            }
            return result;
        }

        public int EditOrderStatus(OrderStatus orderStatus)
        {
            int result = 0;
            var model = db.OrderStatus.Where(ord => ord.OrderStatusId == orderStatus.OrderStatusId).FirstOrDefault();
            if (model != null)
            {
                model.Status = orderStatus.Status;
                result = db.SaveChanges();
            }
            return result;
        }

        public IEnumerable<OrderStatus> GetAllOrderStatus()
        {
            var model = (from orderstatus in db.OrderStatus
                         select orderstatus).ToList();
            return model;
        }

        public OrderStatus GetOrderStatusById(int id)
        {
            return db.OrderStatus.Where(x => x.OrderStatusId == id).SingleOrDefault();
        }
    }
}

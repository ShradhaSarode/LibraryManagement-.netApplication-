using LibraryManagement.Data;
using LibraryManagement.Models;
using Microsoft.CodeAnalysis;

namespace LibraryManagement.Repositories
{
    public class CartRepo : ICartRepo
    {
        private readonly ApplicationDbContext db;
        public CartRepo(ApplicationDbContext db)
        {
            this.db = db;
        }
        public int AddToCart(Cart cart)
        {
            //bool exists = CheckIfExists(cart);
            //if (!exists)
            //{
            //    db.Carts.Add(cart);
            //    int res = db.SaveChanges();
            //    return res;
            //}
            //else
            //{
            //    return 2;
            //}
            try
            {
                var bk = db.Books.FirstOrDefault(p => p.BookID == cart.BookID);

                if (bk != null && bk.Stock >= cart.Quantity && cart.Quantity > 0)
                {
                    var existingCartItem = db.Carts.FirstOrDefault(x => x.UserID == cart.UserID && x.BookID == cart.BookID);
                    if (existingCartItem == null)
                    {
                        db.Carts.Add(cart);
                    }
                    else
                    {
                        existingCartItem.Quantity += cart.Quantity;
                    }
                    return db.SaveChanges();
                }
                else if (bk == null)
                {
                    throw new Exception($"Book with ID {cart.BookID} not found.");
                }
                else if (bk.Stock < cart.Quantity)
                {
                    throw new Exception($"Insufficient stock for product '{bk.Title}'. Available stock: {bk.Stock}");
                }
                else if (cart.Quantity <= 0)
                {
                    throw new Exception($"Quantity must be greater than zero.");
                }
                else
                {
                    throw new Exception("Unknown error occurred while adding to cart.");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions as per your application's error handling strategy
                throw new Exception("Failed to add item to cart.", ex);
            }
        }

        public bool CheckIfExists(Cart cart)
        {
            bool exists = false;
            var data = db.Carts.Where(x => x.UserID == cart.UserID).ToList();
            foreach (var item in data)
            {
                if (item.BookID == cart.BookID)
                {
                    exists = true;
                    break;
                }
                else
                {
                    exists = false;
                }
            }
            return exists;
        }

        public BookCart ConfirmOrder(int id)
        {
            var result = (from c in db.Carts
                          join p in db.Books on c.BookID equals p.BookID
                          where c.CartID == id
                          select new BookCart
                          {
                              BookID = p.BookID,
                              Title = p.Title,
                              Price = p.Price,
                              CoverImage = p.CoverImage,
                              Quantity = 1,
                              CartID = c.CartID,
                              UserID = c.UserID
                          }).FirstOrDefault();
            return result;
        }

        public int GetCartCount(int userid)
        {
            var res = (from c in db.Carts
                       where c.UserID == userid
                       select c).Count();
            return res;
        }

        public IEnumerable<BookCart> GetCartItems(int userid)
        {
            var res = (from c in db.Carts
                       join b in db.Books
                       on c.BookID equals b.BookID
                       where c.UserID == userid
                       select new BookCart
                       {
                           CartID = c.CartID,
                           BookID = b.BookID,
                           UserID = c.UserID,
                           Title = b.Title,
                           CoverImage = b.CoverImage,
                           Price = b.Price,
                           Quantity = c.Quantity
                       }) .ToList();
            return res;
        }

        public int PlaceOrder(Orders order)
        {
            //db.Orders.Add(order);
            //return db.SaveChanges();
            try
            {
                foreach (var item in order.OrderItems)
                {
                    var bk = db.Books.FirstOrDefault(p => p.BookID == item.BookID);

                    if (bk != null)
                    {
                        // Check if sufficient stock is available
                        if (bk.Stock >= item.Quantity)
                        {
                            // Decrease product stock
                            bk.Stock -= item.Quantity;
                        }
                        else
                        {
                            // Insufficient stock, return an error status or message
                            return -1; // or you can throw an exception here if needed
                        }
                    }
                    else
                    {
                        throw new Exception($"Book with ID {item.BookID} not found.");
                    }
                }

                // Add order to Orders table
                db.Orders.Add(order);
                db.SaveChanges();

                return order.OrderId;
            }
            catch (Exception ex)
            {
                // Handle exceptions as per your application's error handling strategy
                throw new Exception("Failed to place order.", ex);
            }
        }

        public int RemoveFromCart(int id)
        {
            int res = 0;
            var cart = db.Carts.Where(x => x.CartID == id).FirstOrDefault();
            if (cart != null)
            {
                db.Carts.Remove(cart);
                res = db.SaveChanges();
            }
            return res;
        }

        public int RemoveFromCartAfterOrder(int userid, int bookid)
        {
            var result = db.Carts.FirstOrDefault(c => c.UserID == userid && c.BookID == bookid);
            if (result != null)
            {
                db.Carts.Remove(result);
                return db.SaveChanges();
            }
            return 0;
        }

        public int UpdateQuantity(int cartId, int quantity)
        {
            var cartItem = db.Carts.FirstOrDefault(c => c.CartID == cartId);
            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                return db.SaveChanges();
            }
            return 0; // Cart item not found
        }
    }
}

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
            bool exists = CheckIfExists(cart);
            if (!exists)
            {
                db.Carts.Add(cart);
                int res = db.SaveChanges();
                return res;
            }
            else
            {
                // If the product already exists in the cart, update its quantity instead
                var existingCartItem = db.Carts.FirstOrDefault(c => c.UserID == cart.UserID && c.BookID == cart.BookID);
                if (existingCartItem != null)
                {
                    existingCartItem.Quantity += cart.Quantity;
                    return db.SaveChanges();
                }
                else
                {
                    // Handle case where the existing cart item is null
                    return 0;
                }
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

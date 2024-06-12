using LibraryManagement.Models;

namespace LibraryManagement.Repositories
{
    public interface ICartRepo
    {
        bool CheckIfExists(Cart cart);
        public int AddToCart(Cart cart);
        public IEnumerable<BookCart> GetCartItems(int userid);

        public int RemoveFromCart(int id);

        // public BookCart ConfirmOrder(int id);

        // public int PlaceOrder(Orders order);

        public int RemoveFromCartAfterOrder(int userid, int bookid);

        public int GetCartCount(int userid);

        int UpdateQuantity(int cartId, int quantity);
    }
}

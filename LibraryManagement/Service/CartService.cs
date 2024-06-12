using LibraryManagement.Models;
using LibraryManagement.Repositories;
using Microsoft.CodeAnalysis;

namespace LibraryManagement.Service
{
    public class CartService : ICartService
    {
        private readonly ICartRepo repo;

        public CartService(ICartRepo repo)
        {
            this.repo = repo;
        }
        public int AddToCart(Cart cart)
        {
            return repo.AddToCart(cart);
        }

        public bool CheckIfExists(Cart cart)
        {
            return repo.CheckIfExists(cart);
        }

        public int GetCartCount(int userid)
        {
            return repo.GetCartCount(userid);
        }

        public IEnumerable<BookCart> GetCartItems(int userid)
        {
            return repo.GetCartItems(userid);
        }

        public int RemoveFromCart(int id)
        {
            return repo.RemoveFromCart(id);
        }

        public int RemoveFromCartAfterOrder(int userid, int bookid)
        {
            return repo.RemoveFromCartAfterOrder(userid, bookid);
        }

        public int UpdateQuantity(int cartId, int quantity)
        {
            return repo.UpdateQuantity(cartId, quantity);
        }
    }
}

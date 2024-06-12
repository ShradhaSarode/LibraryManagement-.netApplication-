using LibraryManagement.Models;
using LibraryManagement.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryManagement.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService cartService;
        private readonly IUsersService userService;
        private readonly IBookService bkService;

        public CartController(ICartService cartService, IUsersService userService, IBookService bkService)
        {
            this.cartService = cartService;
            this.userService = userService;
            this.bkService = bkService;
        }
        private int GetCurrentUserId()
        {
            //var email = User.FindFirst(ClaimTypes.Email)?.Value;
            //var user = userService.GetUserByEmail(email);
            //return user?.Id ?? 0;
            // Get the user's ID from the claims
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
            else
            {
                // If user ID not found or cannot be parsed, return a default value or handle the error accordingly
                // For example, you can redirect the user to a login page or return -1 indicating an error
                return -1;
            }
        }
        // GET: CartController
        public ActionResult Index()
        {
            int userId = GetCurrentUserId();
            var cartItems = cartService.GetCartItems(userId);
            return View(cartItems);
        }

        // GET: CartController/Details/5
        public ActionResult Details(int id)
        {
            var bk = bkService.GetBookById(id);
            return View(bk);
        }

        // POST: CartController/AddToCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int productId, int quantity)
        {
            try
            {
                int userId = GetCurrentUserId();

                // Create a Cart object to add to the database
                var cart = new Cart
                {
                    UserID = userId,
                    BookID = productId,
                    Quantity = quantity
                };

                // Add the product to the cart using the CartService
                int result = cartService.AddToCart(cart);

                // Check the result of the operation
                if (result > 0)
                {
                    // Product successfully added to the cart
                    return RedirectToAction(nameof(Index));
                }
                else if (result == 2)
                {
                    // Product already in cart
                    ViewBag.ErrorMsg = "Product already in cart.";
                }
                else
                {
                    // Failed to add product to cart
                    ViewBag.ErrorMsg = "Failed to add product to cart.";
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                ViewBag.ErrorMsg = "An error occurred while adding the product to the cart. Please try again later.";
            }

            // Redirect the user back to the Index view
            return RedirectToAction(nameof(Index));
        }

        // POST: CartController/RemoveFromCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveFromCart(int cartId)
        {
            try
            {
                int result = cartService.RemoveFromCart(cartId);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.ErrorMsg = "Failed to remove product from cart.";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }
        }

        public IActionResult UpdateQuantity(int cartId, int quantity)
        {
            try
            {
                int result = cartService.UpdateQuantity(cartId, quantity);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.ErrorMsg = "Failed to update quantity.";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }
        }

    }
}

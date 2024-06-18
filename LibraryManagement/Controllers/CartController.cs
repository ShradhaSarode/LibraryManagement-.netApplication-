using LibraryManagement.Models;
using LibraryManagement.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using System.Security.Claims;

namespace LibraryManagement.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService cartService;
        private readonly IUsersService userService;
        private readonly IBookService bkService;
        private readonly IOrderService orderService;
        private readonly IOrderStatusService orderStatusService;

        public CartController(ICartService cartService,
            IUsersService userService,
            IBookService bkService,
            IOrderService orderService,
            IOrderStatusService orderStatusService)
        {
            this.cartService = cartService;
            this.userService = userService;
            this.bkService = bkService;
            this.orderService = orderService;
            this.orderStatusService = orderStatusService;
        }
        private int GetCurrentUserId()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                throw new Exception("User is not authenticated");
            }

            return userId.Value;
        }
        // GET: CartController
        public ActionResult Index()
        {
            //int userId = GetCurrentUserId();
            //var cartItems = cartService.GetCartItems(userId);
            //return View(cartItems);
            try
            {
                int userId = GetCurrentUserId();
                var cartItems = cartService.GetCartItems(userId);
                // Update cart count
                GetCartCount();
                return View(cartItems);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        public IActionResult GetCartCount()
        {
            try
            {
                int userId = GetCurrentUserId();
                int cartCount = cartService.GetCartCount(userId);
                ViewBag.CartCount = cartCount;
                return View(Index);
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                ViewBag.Error = ex.Message;
                return View();
            }
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
        public IActionResult AddToCart(int bookId, int userId)
        {
            try
            {
                userId = GetCurrentUserId();

                // Fetch product details
                var book = bkService.GetBookById(bookId);
                if (book.Stock <= 0)
                {
                    // If stock is not available, show a notification message
                    TempData["NotifyMessage"] = "Stock not available. You will be notified when the product is back in stock.";
                    return RedirectToAction("ProductList", "Product");
                }

                var cart = new Cart
                {
                    UserID = userId,
                    BookID = bookId,

                };
                var result = cartService.AddToCart(cart);
                if (result > 0)
                {
                    // Successfully added to cart
                    return RedirectToAction("Index");
                }
                else
                {
                    // Handle failure to add to cart
                    ViewBag.ErrorMessage = "Unable to add product to cart. Please try again.";
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Index");
            }
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
                    ViewBag.ErrorMsg = "Failed to remove book from cart.";
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

        public ActionResult ConfirmOrder()
        {

            return View();
        }
        [HttpPost]
        public IActionResult ConfirmOrder(int cartId)
        {
            try
            {
                var cartItem = cartService.ConfirmOrder(cartId);
                if (cartItem != null)
                {
                    var orderStatuses = orderStatusService.GetAllOrderStatus()
                    .Select(os => new SelectListItem
                    {
                        Value = os.OrderStatusId.ToString(),
                        Text = os.Status
                    }).ToList();

                    ViewBag.OrderStatuses = orderStatuses;
                    return View("ConfirmOrder", cartItem);
                }

                ViewBag.ErrorMsg = "Failed to confirm order.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PlaceOrder(int bookId, int quantity, int orderStatusId)
        {
            try
            {
                int userId = GetCurrentUserId();

                // Retrieve cart items for the user
                var cartItems = cartService.GetCartItems(userId);

                // Calculate total amount
                decimal totalAmount = cartItems.Sum(item => item.Price * item.Quantity);

                // Create new order
                var order = new Orders
                {
                    UserId = userId,
                    OrderDate = DateTime.Now,
                    TotalAmount = totalAmount,
                    OrderItems = cartItems.Select(item => new OrderItems
                    {
                        BookID = item.BookID,
                        OrderStatusId = orderStatusId,
                        Quantity = item.Quantity,
                        Price = item.Price
                    }).ToList()
                };
                // Place the order
                cartService.PlaceOrder(order);

                // Clear the cart after order placement
                foreach (var item in cartItems)
                {
                    cartService.RemoveFromCartAfterOrder(userId, item.BookID);
                }

                return RedirectToAction("OrderSuccess", "Order");
            }

            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return RedirectToAction("Index", "Cart");
            }
        }

        public IActionResult OrderList()
        {
            try
            {
                int userId = GetCurrentUserId();
                var orders = orderService.GetOrders(userId);
                if (orders == null)
                {
                    ViewBag.ErrorMessage = "No orders found.";
                    return View("Error");
                }
                return View(orders);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }

    }
}

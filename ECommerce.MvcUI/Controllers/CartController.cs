using ECommerce.Business.Abstract;
using ECommerce.Entities;
using ECommerce.MvcUI.Identity;
using ECommerce.MvcUI.Models;
using ECommerce.MvcUI.Models.OrderModel;
using ECommerce.MvcUI.Payments;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.MvcUI.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICustomerAddressService _customerAddressService;
        private readonly ICustomerCardService _customerCardService;
        public CartController(ICartService cartService, IOrderService orderService, UserManager<ApplicationUser> userManager, ICustomerAddressService customerAddressService, ICustomerCardService customerCardService)
        {
            _cartService = cartService;
            _orderService = orderService;
            _userManager = userManager;
            _customerAddressService = customerAddressService;
            _customerCardService = customerCardService;
        }

        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var cart = _cartService.GetCartByUserId(userId);

            if (cart == null)
            {
                cart = _cartService.InitializeCart(userId);
            }

            return View(new CartModel()
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(i => new CartItemModel
                {
                    CartItemId = i.Id,
                    ProductId = i.ProductId,
                    Name = i.Product.Name,
                    Price = (decimal)i.Product.Price,
                    DiscountedPrice = (decimal)i.Product.DiscountedPrice,
                    ImageUrl = i.Product.ImageUrl,
                    Quantity = i.Quantity

                }).ToList() ?? new List<CartItemModel>()
            });
        }

        public IActionResult Wishlist()
        {
            var cart = _cartService.GetCartByUserId(_userManager.GetUserId(User));

            return View(new CartModel()
            {
                CartId = cart.Id,
                Wishlists = cart.Wishlists.Select(i => new WishlistModel
                {
                    WishlistId = i.Id,
                    ProductId = i.ProductId,
                    Name = i.Product.Name,
                    Price = (decimal)i.Product.Price,
                    DiscountedPrice = (decimal)i.Product.DiscountedPrice,
                    ImageUrl = i.Product.ImageUrl

                }).ToList()
            });


        }

        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity)
        {

            _cartService.AddToCart(_userManager.GetUserId(User), productId, quantity);
            return RedirectToAction("Index");

        }

        [HttpPost]
        public IActionResult DeleteFromCart(int productId)
        {
            _cartService.DeleteFromCart(_userManager.GetUserId(User), productId);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult CheckOut()
        {

            var userId = _userManager.GetUserId(User);
            var cart = _cartService.GetCartByUserId(userId);
            var model = new CartModel()
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(i => new CartItemModel
                {
                    CartItemId = i.Id,
                    ProductId = i.ProductId,
                    Name = i.Product.Name,
                    Price = (decimal)i.Product.Price,
                    DiscountedPrice = (decimal)i.Product.DiscountedPrice,
                    ImageUrl = i.Product.ImageUrl,
                    Quantity = i.Quantity
                }).ToList() ?? new List<CartItemModel>()
            };
            var orderModel = new OrderModel
            {
                CartModel = model
            };


            return View(orderModel);
        }

        [HttpPost]
        public async Task<IActionResult> CheckOut(OrderModel model)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var user = await _userManager.FindByIdAsync(userId);
                var cart = _cartService.GetCartByUserId(userId);


                var cartModel = new CartModel
                {
                    CartId = cart.Id,
                    CartItems = cart.CartItems.Select(i => new CartItemModel
                    {
                        CartItemId = i.Id,
                        ProductId = i.ProductId,
                        Name = i.Product.Name,
                        Price = (decimal)i.Product.Price,
                        DiscountedPrice = (decimal)i.Product.DiscountedPrice,
                        ImageUrl = i.Product.ImageUrl,
                        Quantity = i.Quantity
                    }).ToList()
                };


                var orderModel = new Order
                {
                    Id = model.Id,
                    CardName = "IyziCheckOut",
                    ExpirationMonth="00",
                    ExpirationYear="00",
                    Cvv="***",
                    CardNumber="****************",
                    ConversationId = Guid.NewGuid().ToString(),
                    PaymentToken = "IyziPay",
                    OrderNumber = new Random().Next(100000, 999999).ToString(),
                    OrderState = "Pending",
                    OrderDate = DateTime.Now.ToString(),
                    UserId = userId,
                    Status = true,
                    PaymentTypes = EnumPaymentTypes.CreditCard,
                    OrderNote =" model.OrderNote",
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    City = model.City,
                    Phone = model.Phone,
                    Email = string.IsNullOrEmpty(model.Email) ? user.Email : model.Email,
                  
                    
                };
                _orderService.Add(orderModel);

                var iyzicoService = new IyzicoService();
                var checkoutFormHtml = await iyzicoService.InitializeCheckOutForm(
                    cartModel,
                    orderModel.Email,
                    "https://webhook.site/b11652e2-01da-409b-8d43-00f7faac3cf1"
                );
                //ngrok olusturup tünel adresi verecegiz.
                //localhost:7061/Cart/PaymentCallback 
                // local.webhook.com  olarak gönder 
                // local.trendyol.com olarak gönder
                model.CartModel = cartModel;

                ViewBag.CheckoutFormHtml = checkoutFormHtml;
     
                return View("Checkout", orderModel);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Ödeme başlatılamadı: " + ex.Message;
                return View("Checkout", model);

            }


        }

    }
}

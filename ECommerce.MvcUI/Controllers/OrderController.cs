using ECommerce.Business.Abstract;
using ECommerce.MvcUI.Identity;
using ECommerce.MvcUI.Models;
using ECommerce.MvcUI.Models.OrderModel;
using ECommerce.MvcUI.Models.OrderModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.MvcUI.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(IOrderService orderService, UserManager<ApplicationUser> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View(new OrderListModelAdmin
            {
                Orders = _orderService.GetAll().Where(x => x.Status == true).ToList()
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult OrderDetails(int id)
        {
            var entity = _orderService.GetById(id);
            ViewBag.OrderState = new List<string> { "Pending", "The product is being prepared.", "It was delivered to the cargo.", "The cargo is on its way.", "Was Delivered.", "It is Cancelled.", "Returned" };
            return View(new OrderModel
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                OrderNumber = entity.OrderNumber,
                OrderDate = Convert.ToDateTime(entity.OrderDate),
                Email = entity.Email,
                OrderState = entity.OrderState,
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult OrderDetails(OrderModel model)
        {
            var entity = _orderService.GetById(model.Id);
            ViewBag.OrderState = new List<string> { "Pending", "The product is being prepared.", "It was delivered to the cargo.", "The cargo is on its way.", "Was Delivered.", "It is Cancelled.", "Returned" };
            if (entity == null)
            {
                return NotFound();
            }
            entity.OrderState = model.OrderState;
            _orderService.Update(entity);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(OrderModel model)
        {

            var entity = _orderService.GetById(model.Id);
            if (entity == null)
            {
                return NotFound();
            }
            entity.Status = false;
            _orderService.Update(entity);
            return RedirectToAction("Index");

        }

        [Authorize]
        public IActionResult GetOrders()
        {
          
            var orders = _orderService.GetOrder(_userManager.GetUserId(User)).OrderByDescending(x=>x.Id);
            var orderListModel = new List<OrderListModel>();
            OrderListModel orderModel;
            foreach(var x in orders)
            {
                orderModel = new OrderListModel();
                orderModel.OrderId = x.Id;
                orderModel.OrderNumber = x.OrderNumber;
                orderModel.OrderDate = Convert.ToDateTime( x.OrderDate);
                orderModel.OrderNote= x.OrderNote;
                orderModel.Phone= x.Phone;
                orderModel.Email= x.Email;
                orderModel.Address= x.Address;
                orderModel.City= x.City;
                orderModel.FirstName= x.FirstName;
                orderModel.LastName= x.LastName;
                orderModel.OrderState= x.OrderState;
                orderModel.OrderItems =x.OrderItems.Select(i=>new OrderItemModel()
                {
                    OrderItemId=i.Id,
                    Name=i.Product.Name,
                    DiscountedPrice =i.DiscountPrice,
                    Price =i.Price,
                    Quantity =i.Quantity,
                    ImageUrl =i.Product.ImageUrl
                }).ToList();
                orderListModel.Add(orderModel);
            }
            return View(orderListModel);

        }




    }
}
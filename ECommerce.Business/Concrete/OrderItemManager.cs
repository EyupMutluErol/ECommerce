
using ECommerce.Business.Abstract;
using ECommerce.Data.Abstract;
using ECommerce.Entities;

namespace ECommerce.Business.Concrete;

public class OrderItemManager : IOrderItemService
{
    private readonly IOrderItemRepository _orderItemRepository;

    public OrderItemManager(IOrderItemRepository orderItemRepository)
    {
        _orderItemRepository = orderItemRepository;
    }

    public List<OrderItem> GetAll()
    {
       return _orderItemRepository.GetAll();
    }
}
//CategoryManager 
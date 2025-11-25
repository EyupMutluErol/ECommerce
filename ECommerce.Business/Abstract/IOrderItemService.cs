

using ECommerce.Entities;

namespace ECommerce.Business.Abstract;

public interface IOrderItemService
{
    List<OrderItem> GetAll();
}

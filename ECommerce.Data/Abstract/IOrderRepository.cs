
using ECommerce.Entities;

namespace ECommerce.Data.Abstract;

public interface IOrderRepository:IGenericRepository<Order>
{

    List<Order> GetOrders(string userId);
}

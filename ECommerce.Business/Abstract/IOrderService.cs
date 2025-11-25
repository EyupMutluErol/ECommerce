
using ECommerce.Entities;

namespace ECommerce.Business.Abstract
{
    public interface IOrderService
    {
        List<Order>GetOrder(string userId);
        List<Order> GetAll();
        Order GetById(int id);
        void Add(Order order);
        void Update(Order order);
    }
}

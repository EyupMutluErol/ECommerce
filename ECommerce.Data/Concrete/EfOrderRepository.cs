
using ECommerce.Data.Abstract;
using ECommerce.Data.Context;
using ECommerce.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace ECommerce.Data.Concrete
{
    public class EfOrderRepository(AppDbContext _context) : EfGenericRepositoryBase<Order, AppDbContext>(_context), IOrderRepository
    {
        private readonly AppDbContext context = _context;
        public List<Order> GetOrders(string userId)
        {

            IQueryable<Order> orders = _context.Orders.Include(i => i.OrderItems).ThenInclude(i => i.Product);
                if (!string.IsNullOrEmpty(userId))
                {
                    orders=orders.Where(i=>i.UserId==userId);
                }
                //return orders.ToList();

                return orders.OrderByDescending(i=>i.Id).ToList(); //son eklenen sipariş en üstte görünsün diye
        }
    }
}

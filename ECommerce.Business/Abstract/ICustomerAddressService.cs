
using ECommerce.Entities;

namespace ECommerce.Business.Abstract;

public interface ICustomerAddressService
{
    CustomerAddress GetById(int id);
    List<CustomerAddress> GetAll();
    void Add(CustomerAddress customerAddress);
    void Update(CustomerAddress customerAddress);
    void Delete(CustomerAddress customerAddress);
}

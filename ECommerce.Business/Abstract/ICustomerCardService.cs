

using ECommerce.Entities;

namespace ECommerce.Business.Abstract
{
    public interface ICustomerCardService
    {
        CustomerCard GetById(int id);
        List<CustomerCard> GetAll();
        void Add(CustomerCard customerCard);
        void Update(CustomerCard customerCard);
        void Delete(CustomerCard customerCard);
    }
}



using ECommerce.Business.Abstract;
using ECommerce.Data.Abstract;
using ECommerce.Entities;

namespace ECommerce.Business.Concrete;

public class CustomerCardManager : ICustomerCardService
{
    private readonly ICustomerCardRepository _customerCardRepository;

    public CustomerCardManager(ICustomerCardRepository customerCardRepository)
    {
        _customerCardRepository = customerCardRepository;
    }

    public void Add(CustomerCard customerCard)
    {
        _customerCardRepository.Add(customerCard);
    }

    public void Delete(CustomerCard customerCard)
    {
       _customerCardRepository.Delete(customerCard);
    }

    public List<CustomerCard> GetAll()
    {
        return _customerCardRepository.GetAll();    
    }

    public CustomerCard GetById(int id)
    {
        return _customerCardRepository.GetById(id);
    }

    public void Update(CustomerCard customerCard)
    {
        _customerCardRepository.Update(customerCard);
    }
}

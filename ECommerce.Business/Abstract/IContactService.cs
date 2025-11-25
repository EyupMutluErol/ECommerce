
using ECommerce.Entities;

namespace ECommerce.Business.Abstract;

public interface IContactService
{
    Contact GetById(int id);
    List<Contact> GetAll();
    void Add(Contact contact);
    void Update(Contact contact);
    void Delete(Contact contact);
}

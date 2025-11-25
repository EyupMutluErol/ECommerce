using ECommerce.Entities;

namespace ECommerce.Business.Abstract;

public interface ICampaingService
{
    Campaing GetById(int id);
    List<Campaing> GetAll();
    void Add(Campaing campaing);
    void Update(Campaing campaing);
    void Delete(Campaing campaing);

}

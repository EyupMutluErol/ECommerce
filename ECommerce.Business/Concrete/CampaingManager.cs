
using ECommerce.Business.Abstract;
using ECommerce.Data.Abstract;
using ECommerce.Entities;

namespace ECommerce.Business.Concrete
{
    public class CampaingManager : ICampaingService
    {

        private readonly ICampaingRepository _campaingRepository;

        public CampaingManager(ICampaingRepository campaingRepository)
        {
            _campaingRepository = campaingRepository;
        }

        public void Add(Campaing campaing)
        {
            _campaingRepository.Add(campaing);
        }

        public void Delete(Campaing campaing)
        {
            _campaingRepository.Delete(campaing);
        }

        public List<Campaing> GetAll()
        {
            return _campaingRepository.GetAll();
        }

        public Campaing GetById(int id)
        {
            return _campaingRepository.GetById(id);
        }

        public void Update(Campaing campaing)
        {
            _campaingRepository.Update(campaing);
        }
    }
}

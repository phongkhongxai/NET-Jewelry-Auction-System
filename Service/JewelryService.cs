using BusinessObjects;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class JewelryService : IJewelryService
    {
        private readonly IJewelryRepository jewelryRepository;
        public JewelryService()
        {
            jewelryRepository = new JewelryRepository();
        }
        public void CreateJewelry(Jewelry jewelry)
        {
            jewelryRepository.CreateJewelry(jewelry);   
        }

        public void DeleteJewelry(Jewelry jewelry)
        {
            jewelryRepository.DeleteJewelry(jewelry);
        }

        public List<Jewelry> GetAllJewelries()
        {
            return jewelryRepository.GetAllJewelries();
        }

        public List<Jewelry> GetJewelriesByPage(int page, int pageSize)
        {
            return jewelryRepository.GetJewelriesByPage(page, pageSize);
        }

        public Jewelry GetJewelryById(int id)
        {
            return jewelryRepository.GetJewelryById(id);
        }

        public int GetTotalJewelries()
        {
            return jewelryRepository.GetTotalJewelries();
        }

        public void UpdateJewelry(Jewelry jewelry)
        {
            jewelryRepository.UpdateJewelry(jewelry);
        }
    }
}

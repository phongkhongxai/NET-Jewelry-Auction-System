using BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class JewelryRepository : IJewelryRepository
    {
        public void CreateJewelry(Jewelry jewelry)
        {
            JewelryDAO.CreateJewelry(jewelry);
        }

        public void DeleteJewelry(Jewelry jewelry)
        {
            JewelryDAO.DeleteJewelry(jewelry);
        }

        public List<Jewelry> GetAllJewelries()
        {
            return JewelryDAO.GetJewelryList();
        }

        public Jewelry GetJewelryById(int id)
        {
            return JewelryDAO.GetJewelryById(id);
        }

        public void UpdateJewelry(Jewelry jewelry)
        {
            JewelryDAO.UpdateJewelry(jewelry);
        }

        //paging method
        public List<Jewelry> GetJewelriesByPage(int page, int pageSize)
        {
            return JewelryDAO.GetJewelryByPage(page, pageSize);
        }

        public int GetTotalJewelries()
        {
            return JewelryDAO.GetTotalJewelry();
        }
    }
}

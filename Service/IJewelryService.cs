using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
	public interface IJewelryService
	{
        public List<Jewelry> GetAllJewelries();
        public Jewelry GetJewelryById(int id);
        public void CreateJewelry(Jewelry jewelry);
        public void UpdateJewelry(Jewelry jewelry);
        public void DeleteJewelry(Jewelry jewelry);

        //Paging method
        public List<Jewelry> GetJewelriesByPage(int page, int pageSize);
        public int GetTotalJewelries();
    }
}

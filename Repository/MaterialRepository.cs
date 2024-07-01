using BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
	public class MaterialRepository : IMaterialRepository
	{
		public void CreateMaterial(Material material)
		{
			MaterialDAO.CreateMaterial(material);	
		}

		public Material GetMaterial(int id)
		{
			return MaterialDAO.GetMaterialById(id);
		}

		public List<Material> GetMaterials()
		{
			return MaterialDAO.GetMaterialList();
		}

		public void UpdateMaterial(Material material)
		{
			MaterialDAO.UpdateMaterial(material);
		}
	}
}

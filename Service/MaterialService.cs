using BusinessObjects;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
	public class MaterialService : IMaterialService

	{

		private readonly IMaterialRepository materialRepository;

		public MaterialService()
		{
			materialRepository = new MaterialRepository();
		}
		public void CreateMaterial(Material material)
		{
			materialRepository.CreateMaterial(material);
		}

		public Material GetMaterial(int id)
		{
			return materialRepository.GetMaterial(id);
		}

		public List<Material> GetMaterials()
		{
			return materialRepository.GetMaterials();
		}

		public void UpdateMaterial(Material material)
		{
			materialRepository.UpdateMaterial(material);
		}
	}
}

using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
	public interface IMaterialService
	{
		public List<Material> GetMaterials();
		public Material GetMaterial(int id);
		public void CreateMaterial(Material material);
		public void UpdateMaterial(Material material);
	}
}

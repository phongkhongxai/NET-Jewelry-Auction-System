using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
	public class MaterialDAO
	{
		public static List<Material> GetMaterialList()
		{
			var list = new List<Material>();
			try
			{
				using var db = new GroupProjectPRN221();
				list = db.Materials.ToList();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return list;
		}

		public static Material GetMaterialById(int id)
		{
			using var db = new GroupProjectPRN221();
			return db.Materials.FirstOrDefault(j => j.Id == id);
		}

		public static void CreateMaterial(Material material)
		{
			try
			{
				using var db = new GroupProjectPRN221();
				db.Materials.Add(material);
				db.SaveChanges();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		} 

		public static void UpdateMaterial(Material material)
		{
			try
			{
				using var db = new GroupProjectPRN221();
				db.Entry<Material>(material).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
				db.SaveChanges();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}

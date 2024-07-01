using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
	public class JewelryDAO
	{
		public static List<Jewelry> GetJewelryList()
		{
			var list = new List<Jewelry>();
			try
			{
				using var db = new GroupProjectPRN221();
				list = db.Jewelries.Include(j => j.AuctionRequest).ToList();
			} catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return list;
		}

		public static Jewelry GetJewelryById(int id) 
		{
			using var db = new GroupProjectPRN221();
			return db.Jewelries.FirstOrDefault(j => j.Id == id);
		}

		public static void CreateJewelry(Jewelry jewelry)
		{
			try
			{
				using var db = new GroupProjectPRN221();
				db.Jewelries.Add(jewelry);
				db.SaveChanges();
			} catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public static void DeleteJewelry(Jewelry jewelry) 
		{
			try
			{
				using var db = new GroupProjectPRN221();
				var c = db.Jewelries.FirstOrDefault(a => a.Id == jewelry.Id);
				
				if(c != null)
				{
					c.IsDelete = true;
					db.SaveChanges();
				} else
				{
					throw new Exception("Not found Jewelry");
				}

			} catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public static void UpdateJewelry(Jewelry jewelry)
		{
			try
			{
				using var db = new GroupProjectPRN221();
				db.Entry<Jewelry>(jewelry).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
				db.SaveChanges();
			} catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}

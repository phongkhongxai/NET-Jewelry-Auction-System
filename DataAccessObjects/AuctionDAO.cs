using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
	public class AuctionDAO
	{
		public static List<Auction> GetAllAuctions()
		{
			var list = new List<Auction>();
			try
			{
				using var db = new GroupProjectPRN221();
				list = db.Auctions.Include(c => c.Jewelry).ToList();
			} catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return list;
		}

		public static Auction GetAuctionById(int id)
		{
			using var db = new GroupProjectPRN221();
			return db.Auctions.SingleOrDefault(a => a.Id == id);
		}

		public static void CreateAuction(Auction auction)
		{
			try
			{
				using var db = new GroupProjectPRN221();
				db.Auctions.Add(auction);
				db.SaveChanges();
			} catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public static void UpdateAuction(Auction auction)
		{
			try
			{
				using var db = new GroupProjectPRN221();
				db.Entry<Auction>(auction).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			} catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public static void DeleteAuction(Auction auction)
		{
			try
			{
				using var db = new GroupProjectPRN221();
				var c = db.Auctions.FirstOrDefault(a => a.Id == auction.Id);
				if (c != null)
				{
					c.IsDelete = true;
					db.SaveChanges();
				} else
				{
					throw new Exception("Auction not found");
				}
			} catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public static List<Bid> GetBidForAuctionId(int auctionId)
		{
            var list = new List<Bid>();
            try
			{
                using var db = new GroupProjectPRN221();
                list = db.Bids.Where(b => b.AuctionId == auctionId).ToList();
            } catch (Exception ex)
			{
                throw new Exception(ex.Message);
            }
            return list;
        }

        public static List<Auction> GetAuctionByUserID(int userId)
        {
            var list = new List<Auction>();	
            try
            {
                using var db = new GroupProjectPRN221();
                list = db.Auctions.Include(c => c.UserAuctions)
                    .Where(a => a.UserAuctions.Any(u => u.UserId == userId)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
    }
}

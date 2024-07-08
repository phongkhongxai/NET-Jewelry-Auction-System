using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class BidDAO
    {
        public static void AddBid(Bid bid)
        {
            try
            {
                using var db = new GroupProjectPRN221();
                db.Bids.Add(bid);
                db.SaveChanges();
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<Bid> GetAllBids()
        {
			var list = new List<Bid>();
			try
			{
				using var db = new GroupProjectPRN221();
				list = db.Bids.Include(b => b.Auction).Include(b => b.User).ToList();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return list;
		}

        public static Bid GetBidById(int bidId)
        {
			using var db = new GroupProjectPRN221();
			return db.Bids.Include(b => b.Auction).Include(b => b.User).FirstOrDefault(b => b.Id == bidId);
		}

        public static void UpdateBid(Bid bid)
        {
            try
            {
                using var db = new GroupProjectPRN221();
                db.Entry(bid).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteBid(Bid bid)
        {
            try
            {
                using var db = new GroupProjectPRN221();
                var c = db.Bids.FirstOrDefault(b => b.Id == bid.Id);
                if(c != null)
                {
                    c.IsDeleted = true;
                    db.SaveChanges();
                }
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<Bid> GetBidsByAuctionId(int auctionId)
        {
            using var db = new GroupProjectPRN221();
            return db.Bids.Where(b => b.AuctionId == auctionId).ToList();
        }
	}
}

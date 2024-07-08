using BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AuctionRepository : IAuctionRepository
    {
        public void CreateAuction(Auction auction)
        {
            AuctionDAO.CreateAuction(auction);
        }

        public void DeleteAuction(Auction auction)
        {
            AuctionDAO.DeleteAuction(auction);
        }

        public List<Auction> GetAllAuctions()
        {
            return AuctionDAO.GetAllAuctions();
        }

        public Auction GetAuctionById(int id)
        {
            return AuctionDAO.GetAuctionById(id);
        }

		public List<Auction> GetAuctionByStatus(string status)
		{
            return AuctionDAO.GetAuctionByStatus(status);
		}

		
		public List<Auction> GetAuctionByJewelryId(int id)
		{
			return AuctionDAO.GetAuctionByJewelryId(id);
		}

		public List<Auction> GetAuctionByUserID(int id)
        {
            return AuctionDAO.GetAuctionByUserID(id);
        }

        public List<Bid> GetBidForAuction(int auctionId)
        {
            return AuctionDAO.GetBidForAuctionId(auctionId);
        }

        public void UpdateAuction(Auction auction)
        {
            AuctionDAO.UpdateAuction(auction);
        }

		public void UpdateAuctionStatus(int auctionId, string status)
		{
			AuctionDAO.UpdateAuctionStatus(auctionId, status);
		}
	}
}

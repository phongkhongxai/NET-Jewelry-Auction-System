using BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BidRepository : IBidRepository
    {
        public void AddBid(Bid bid)
        {
            BidDAO.AddBid(bid);
        }

        public void DeleteBid(Bid bid)
        {
            BidDAO.DeleteBid(bid);
        }

        public List<Bid> GetAllBids()
        {
            return BidDAO.GetAllBids();
        }

        public Bid GetBidById(int bidId)
        {
            return BidDAO.GetBidById(bidId);
        }

        public List<Bid> GetBidsByAuctionId(int auctionId)
        {
            return BidDAO.GetBidsByAuctionId(auctionId);
        }

        public void UpdateBid(Bid bid)
        {
            BidDAO.UpdateBid(bid);
        }
    }
}

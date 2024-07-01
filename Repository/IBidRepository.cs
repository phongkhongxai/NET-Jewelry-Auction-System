using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IBidRepository
    {
        public List<Bid> GetAllBids();
        public Bid GetBidById(int bidId);
        public void AddBid(Bid bid);
        public void UpdateBid(Bid bid);
        public void DeleteBid(Bid bid);
        public List<Bid> GetBidsByAuctionId(int auctionId);
    }
}

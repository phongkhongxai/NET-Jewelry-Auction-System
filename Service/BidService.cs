using BusinessObjects;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BidService : IBidService
    {
        private readonly IBidRepository _bidRepository;
        public BidService()
        {
            _bidRepository = new BidRepository();
        }
        public void AddBid(Bid bid)
        {
            _bidRepository.AddBid(bid); 
        }

        public void DeleteBid(Bid bid)
        {
            _bidRepository.DeleteBid(bid);
        }

        public List<Bid> GetAllBids()
        {
            return _bidRepository.GetAllBids();
        }

		public Bid GetBidById(int bidId)
        {
            return _bidRepository.GetBidById(bidId);
        }

        public List<Bid> GetBidsByAuctionId(int auctionId)
        {
            return _bidRepository.GetBidsByAuctionId(auctionId);
        }

        public void UpdateBid(Bid bid)
        {
            _bidRepository.UpdateBid(bid);
        }
    }
}

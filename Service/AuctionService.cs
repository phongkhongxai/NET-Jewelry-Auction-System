using BusinessObjects;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuctionService : IAuctionService
    {
        private readonly IAuctionRepository _auctionRepository;
        public AuctionService()
        {
            _auctionRepository = new AuctionRepository();
        }
        public void CreateAuction(Auction auction)
        {
            _auctionRepository.CreateAuction(auction);
        }

        public void DeleteAuction(Auction auction)
        {
            _auctionRepository.DeleteAuction(auction);
        }

        public List<Auction> GetAllAuctions()
        {
            return _auctionRepository.GetAllAuctions();
        }

        public Auction GetAuctionById(int id)
        {
            return _auctionRepository.GetAuctionById(id);
        }

        public List<Bid> GetBidForAuction(int auctionId)
        {
           return _auctionRepository.GetBidForAuction(auctionId);
        }

        public void UpdateAuction(Auction auction)
        {
            _auctionRepository.UpdateAuction(auction);
        }
		public List<Auction> Search(string keyword) => _auctionRepository.GetAllAuctions().Where(c => c.Jewelry.Name.Contains(keyword)).ToList();

		public List<Auction> GetAuctionsByUserId(int userId)
		{
			return _auctionRepository.GetAuctionsByUserId(userId);
		}
	}
}

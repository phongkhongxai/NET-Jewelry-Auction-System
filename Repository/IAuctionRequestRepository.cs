using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IAuctionRequestRepository
    {
        public List<AuctionRequest> GetAllAuctionRq();

        public List<AuctionRequest> GetAllAuctionRequestsWithoutJewelry();

		public AuctionRequest GetAuctionRqById(int id);
        public void CreateAuctionRequest(AuctionRequest auctionRequest);
        public void UpdateAuctionRequest(AuctionRequest auctionRequest);
        public void DeleteAuctionRequest(AuctionRequest auctionRequest);
    }
}

using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IAuctionRequestService
    {
        public List<AuctionRequest> GetAllAuctionRq();
        public AuctionRequest GetAuctionRqById(int id);
        public void CreateAuctionRequest(AuctionRequest auctionRequest);
        public void UpdateAuctionRequest(AuctionRequest auctionRequest);
        public void DeleteAuctionRequest(AuctionRequest auctionRequest);
    }
}

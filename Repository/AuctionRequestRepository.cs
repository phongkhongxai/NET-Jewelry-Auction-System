using BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AuctionRequestRepository : IAuctionRequestRepository
    {
        public void CreateAuctionRequest(AuctionRequest auctionRequest)
        {
            AuctionRequestDAO.CreateAuctionRequest(auctionRequest);
        }

        public void DeleteAuctionRequest(AuctionRequest auctionRequest)
        {
            AuctionRequestDAO.DeleteAuctionRequest(auctionRequest);
        }

        public List<AuctionRequest> GetAllAuctionRq()
        {
            return AuctionRequestDAO.GetAllAuctionRequest();
        }

        public AuctionRequest GetAuctionRqById(int id)
        {
            return AuctionRequestDAO.GetAuctionRequestById(id);
        }

        public void UpdateAuctionRequest(AuctionRequest auctionRequest)
        {
            AuctionRequestDAO.UpdateAuctionRequest(auctionRequest);
        }
    }
}

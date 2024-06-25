using BusinessObjects;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuctionRequestService : IAuctionRequestService
    {
        private readonly IAuctionRequestRepository auctionRequestRepository;

        public AuctionRequestService()
        {
            auctionRequestRepository = new AuctionRequestRepository();
        }
        public void CreateAuctionRequest(AuctionRequest auctionRequest)
        {
            auctionRequestRepository.CreateAuctionRequest(auctionRequest);
        }

        public void DeleteAuctionRequest(AuctionRequest auctionRequest)
        {
            auctionRequestRepository.DeleteAuctionRequest(auctionRequest);

        }

        public List<AuctionRequest> GetAllAuctionRq()
        {
             return auctionRequestRepository.GetAllAuctionRq();
        }

        public AuctionRequest GetAuctionRqById(int id)
        {
            return auctionRequestRepository.GetAuctionRqById(id);
        }

        public void UpdateAuctionRequest(AuctionRequest auctionRequest)
        {
            auctionRequestRepository.UpdateAuctionRequest(auctionRequest);

        }
    }
}

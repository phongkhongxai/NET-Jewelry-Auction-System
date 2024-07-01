using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;

namespace Panacea_GroupProject.Pages.AuctionRequestPage
{
    public class RequestApprovalModel : PageModel
    {
        private readonly IAuctionRequestService _auctionRequestService;
        
        public RequestApprovalModel(IAuctionRequestService auctionRequestService)
        {
            _auctionRequestService = auctionRequestService;
        }

        public List<AuctionRequest> AuctionRequests { get; set; }
        public void OnGet()
        {
            AuctionRequests = _auctionRequestService.GetAllAuctionRq();
        }
    }
}

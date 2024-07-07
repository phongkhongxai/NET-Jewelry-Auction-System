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

        public IActionResult OnPost(string action, int RequestId)
        {
            var request = _auctionRequestService.GetAuctionRqById(RequestId);
            if (request != null)
            {
                if (action == "Accept")
                {
                    request.Status = "Approved";
                }
                else if (action == "Deny")
                {
                    request.Status = "Rejected";
                }
                _auctionRequestService.UpdateAuctionRequest(request);
            }
            return RedirectToPage();
        }
    }
}

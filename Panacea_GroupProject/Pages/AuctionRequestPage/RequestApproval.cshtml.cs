using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using System.Security.Claims;

namespace Panacea_GroupProject.Pages.AuctionRequestPage
{
    public class RequestApprovalModel : PageModel
    {
        private readonly IAuctionRequestService _auctionRequestService;
        private readonly IUserService _userService;
        
        public RequestApprovalModel(IAuctionRequestService auctionRequestService, IUserService userService)
        {
            _auctionRequestService = auctionRequestService;
            _userService = userService;
        }

		public User LoggedInUser { get; private set; }

		public List<AuctionRequest> AuctionRequests { get; set; }
        public IActionResult OnGet()
        {
            AuctionRequests = _auctionRequestService.GetAllAuctionRq();
			var claimsIdentity = User.Identity as ClaimsIdentity;
			var userIdClaim = claimsIdentity?.FindFirst("Id");
			if (userIdClaim == null)
			{
				return RedirectToPage("/Accounts/Login");
			}

			LoggedInUser = _userService.GetUserByID(int.Parse(userIdClaim.Value));

			if (LoggedInUser == null)
			{
				return RedirectToPage("/Accounts/Login");
			}
			if (!LoggedInUser.RoleId.Equals(4) && !LoggedInUser.RoleId.Equals(5))
			{
				return RedirectToPage("/Template/Index");

			}
			return Page();
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

using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using System.Security.Claims;

namespace Panacea_GroupProject.Pages.Auctions
{
    public class ManagerAuctionModel : PageModel
    {

		private readonly IUserService _userService;
		private readonly IAuctionService _auctionService;
		private readonly IBidService _bidService;
		private readonly IUserAuctionService _userAuctionService;

		public ManagerAuctionModel(IUserAuctionService userAuctionService, IUserService userService, IAuctionService auctionService, IBidService bidService)
		{
			_userService = userService;
			_auctionService = auctionService;
			_bidService = bidService;
			_userAuctionService = userAuctionService;
		}

		[BindProperty]
		public User LoggedInUser { get; private set; }
		public IList<Auction> UpcomingAuctions { get; set; }
		public IList<Auction> ProcessingAuctions { get; set; }
		public IList<Auction> CompletedAuctions { get; set; }



		public async Task<IActionResult> OnGet()
        {
			await LoadDataAsync();
			if (LoggedInUser == null)
			{
				return RedirectToPage("/Accounts/Login");
			}
			var user = _userService.GetUserByID(LoggedInUser.Id);
			if (user == null)
			{
				return NotFound();
			}
			LoggedInUser = user;
			if (LoggedInUser == null)
			{
				return RedirectToPage("/Accounts/Login");
			}
			if (!user.RoleId.Equals(5))
			{
				return RedirectToPage("/Template/Index"); 
			}
			return Page();
		}

		private async Task LoadDataAsync()
		{
			var claimsIdentity = User.Identity as ClaimsIdentity;
			var userIdClaim = claimsIdentity?.FindFirst("Id");
			if (userIdClaim == null)
			{
				return;
			}
			LoggedInUser = _userService.GetUserByID(int.Parse(userIdClaim.Value));
			UpcomingAuctions = _auctionService.GetAuctionByStatus("Pending");
			ProcessingAuctions = _auctionService.GetAuctionByStatus("Processing");
			CompletedAuctions = _auctionService.GetAuctionByStatus("End");  
		}

		public async Task<IActionResult> OnPostStart(int id)
		{
			await LoadDataAsync();
			if (LoggedInUser == null)
			{
				return RedirectToPage("/Accounts/Login");
			}
			var user = _userService.GetUserByID(LoggedInUser.Id);
			if (user == null)
			{
				return NotFound();
			}
			LoggedInUser = user;
			if (LoggedInUser == null)
			{
				return RedirectToPage("/Accounts/Login");
			}
			if (!user.RoleId.Equals(5))
			{
				return RedirectToPage("/Template/Index");
			}

			var auction = _auctionService.GetAuctionById(id);
			if (auction == null)
			{
				return NotFound();
			}
			if(auction.StartDate > DateTime.Now)
			{
				TempData["AuctionNotStarted"] = "The auction cannot be started yet. The start date is in the future.";
				return RedirectToPage("/Auctions/ManagerAuction");
			}
			_auctionService.UpdateAuctionStatus(auction.Id, "Processing"); 
			return RedirectToPage("/Auctions/BidPrice", new { id = auction.Id });
		}

	}
}

using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Panacea_GroupProject.Helpers;
using Service;
using System.Security.Claims;

namespace Panacea_GroupProject.Pages.Auctions
{
    public class MyAuctionsModel : PageModel
    {
        private readonly IAuctionService _auctionService;
        private readonly IUserService _userService;

		[BindProperty]
		public User LoggedInUser { get; private set; }

		public MyAuctionsModel(IAuctionService auctionService, IUserService userService)
        {
            _auctionService = auctionService;
            _userService = userService;
        }
        public List<Auction> Auctions { get; set; } = new List<Auction>();
        public IActionResult OnGet()
        {
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
			if (LoggedInUser != null)
            {
                Auctions = _auctionService.GetAuctionByUserID(LoggedInUser.Id);
            }
            return Page();
        }
    }
}

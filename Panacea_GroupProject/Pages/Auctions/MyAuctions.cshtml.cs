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
        private readonly IUserAuctionService _userAuctionService;
        private readonly IUserService _userService;
        public User LoggedInUser { get; private set; }

        public MyAuctionsModel(IUserService userService, IUserAuctionService userAuctionService, IAuctionService auctionService)
        {
            _auctionService = auctionService;
            _userAuctionService = userAuctionService;
            _userService = userService;
        }
        public IList<UserAuction> Auctions { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchQuery { get; set; }
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
            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                Auctions = _userAuctionService.GetUserAuctionByUserId(LoggedInUser.Id).Where(c => c.Auction.Jewelry.Name.Contains(SearchQuery)).ToList();
            }
            else
            {
                Auctions = _userAuctionService.GetUserAuctionByUserId(LoggedInUser.Id);
            }
            return Page();
        }
    }
}

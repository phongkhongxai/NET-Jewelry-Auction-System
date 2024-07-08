using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Panacea_GroupProject.Helpers;
using Service;

namespace Panacea_GroupProject.Pages.Auctions
{
    public class MyAuctionsModel : PageModel
    {
        private readonly IAuctionService _auctionService;
        private readonly IUserAuctionService _userAuctionService;
        public User LoggedInUser { get; private set; }

        public MyAuctionsModel(IUserAuctionService userAuctionService, IAuctionService auctionService)
        {
            _auctionService = auctionService;
            _userAuctionService = userAuctionService;
        }
        public IList<UserAuction> Auctions { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchQuery { get; set; }
        public void OnGet()
        {
            LoggedInUser = HttpContext.Session.GetObjectFromJson<User>("LoggedInUser");

            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                Auctions = _userAuctionService.GetUserAuctionByUserId(LoggedInUser.Id).Where(c => c.Auction.Jewelry.Name.Contains(SearchQuery)).ToList();
            }
            else
            {
                Auctions = _userAuctionService.GetUserAuctionByUserId(LoggedInUser.Id);

            }
        }
    }
}

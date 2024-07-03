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

        public MyAuctionsModel(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }
        public List<Auction> Auctions { get; set; } = new List<Auction>();
        public void OnGet()
        {
			var loggedInUser = HttpContext.Session.GetObjectFromJson<User>("LoggedInUser");
			if (loggedInUser != null)
			{
				Auctions = _auctionService.GetAuctionsByUserId(loggedInUser.Id);
			}
		}
    }
}

using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Panacea_GroupProject.Helpers;
using Service;

namespace Panacea_GroupProject.Pages.Auctions
{
    public class BidPriceModel : PageModel
    {
        private readonly IAuctionService _auctionService;
        public BidPriceModel(IAuctionService auctionService, IBidService bidService)
        {
            _auctionService = auctionService;
            _bidService = bidService;
        }
        public User LoggedInUser { get; private set; }
        public Auction currentAuction { get;set; }
		private readonly IBidService _bidService;
        public int AmountBid { get; set; }
		public List<Bid> Bids { get; set; } = new List<Bid>();
        public async Task OnGet(int id)
        {
            LoadDataAsync(id);
            
        }

        private async Task LoadDataAsync(int id)
        {
            LoggedInUser = HttpContext.Session.GetObjectFromJson<User>("LoggedInUser");
            currentAuction = _auctionService.GetAuctionById(id);
            Bids = _auctionService.GetBidForAuction(id);
        }

		public async Task<IActionResult> OnPost()
		{
			LoggedInUser = HttpContext.Session.GetObjectFromJson<User>("LoggedInUser");
			Bid newBid = new Bid{
                UserId=LoggedInUser.Id, 
                Amount= AmountBid, 
                AuctionId = currentAuction.Id, 
                BidTime= DateTime.Now, 
                IsDeleted= false };
			_bidService.AddBid(newBid);
			await LoadDataAsync(currentAuction.Id);
			return Page();
		}
	}
}

using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;

namespace Panacea_GroupProject.Pages.Auctions
{
    public class BidPriceModel : PageModel
    {
        private readonly IAuctionService _auctionService;
        public BidPriceModel(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        public List<Bid> Bids { get; set; } = new List<Bid>();
        public Auction Auction { get; set; }   
        public IActionResult OnGet(int id)
        {
            Auction = _auctionService.GetAuctionById(id);   
            if(Auction == null)
            {
                return NotFound();
            } 
            Bids = _auctionService.GetBidForAuction(id);
            return Page();
        }
    }
}

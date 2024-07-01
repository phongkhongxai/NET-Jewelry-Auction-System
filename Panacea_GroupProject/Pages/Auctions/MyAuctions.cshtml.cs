using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
            Auctions = _auctionService.GetAllAuctions();
        }
    }
}

using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Panacea_GroupProject.Helpers;
using Service;
using System;

namespace Panacea_GroupProject.Pages.Template
{
    public class AuctionsModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IAuctionService _auctionService;
        private readonly IBidService _bidService;
        public AuctionsModel(IUserService userService, IAuctionService auctionService, IBidService bidService)
        {
            _userService = userService;
            _auctionService = auctionService;
            _bidService = bidService;

        }

        public User LoggedInUser { get; private set; }
        public IList<Auction> UpcomingAuctions { get; set; }
        public Auction CurrentAuctions { get; set; }
        public async Task OnGet()
        {
            await LoadDataAsync();
            //UpcomingAuctions =  _auctionService.GetAllAuctions();
            //CurrentAuctions = UpcomingAuctions.FirstOrDefault(c=> c.Status == "Processing");
            //LoggedInUser = HttpContext.Session.GetObjectFromJson<User>("LoggedInUser");
        }

        [BindProperty]
        public decimal BidAmount { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchQuery { get; set; }
        public async Task<IActionResult> OnPost()
        {
            LoggedInUser = HttpContext.Session.GetObjectFromJson<User>("LoggedInUser");
            Bid newBid = new Bid();
            newBid.UserId = LoggedInUser.Id;
            newBid.Amount = 1;
            newBid.AuctionId = 4;
            newBid.BidTime = DateTime.Now;
            newBid.IsDeleted = false;
            _bidService.AddBid(newBid);
            await LoadDataAsync();
            return Page();
        }

        private async Task LoadDataAsync()
        {
            LoggedInUser = HttpContext.Session.GetObjectFromJson<User>("LoggedInUser");
            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                UpcomingAuctions = _auctionService.Search(SearchQuery);
            }
            else
            {
                UpcomingAuctions = _auctionService.GetAllAuctions();
                CurrentAuctions = UpcomingAuctions.FirstOrDefault(c => c.Status == "Processing");
            }
        }
    }
}
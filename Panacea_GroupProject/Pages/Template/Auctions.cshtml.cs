using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Panacea_GroupProject.Helpers;
using Service;
using System;
using System.Security.Claims;

namespace Panacea_GroupProject.Pages.Template
{
    public class AuctionsModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IAuctionService _auctionService;
        private readonly IBidService _bidService;
        private readonly IUserAuctionService _userAuctionService;

        public AuctionsModel(IUserAuctionService userAuctionService, IUserService userService, IAuctionService auctionService, IBidService bidService)
        {
            _userService = userService;
            _auctionService = auctionService;
            _bidService = bidService;
            _userAuctionService = userAuctionService;
        }

        [BindProperty]
        public User LoggedInUser { get; private set; }
        public IList<Auction> UpcomingAuctions { get; set; }
        public Auction CurrentAuctions { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchQuery { get; set; }
        public async Task<IActionResult> OnGet()
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
            await LoadDataAsync();
            //UpcomingAuctions =  _auctionService.GetAllAuctions();
            //CurrentAuctions = UpcomingAuctions.FirstOrDefault(c=> c.Status == "Processing");
            //LoggedInUser = HttpContext.Session.GetObjectFromJson<User>("LoggedInUser");

            return Page();
        }

        [BindProperty]
        public decimal BidAmount { get; set; }

        public async Task<IActionResult> OnPost(int id)
        {
            UserAuction userAuction = new UserAuction()
            {
                UserId = LoggedInUser.Id,
                AuctionId = id
            };
            _userAuctionService.CreateAuction(userAuction);
            await LoadDataAsync();
            return Page();
        }

        private async Task LoadDataAsync()
        {
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

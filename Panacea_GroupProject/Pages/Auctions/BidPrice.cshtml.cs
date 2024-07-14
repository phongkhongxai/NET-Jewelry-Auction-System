using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NuGet.Packaging.Signing;
using Panacea_GroupProject.Helpers;
using Service;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Panacea_GroupProject.Pages.Auctions
{
    public class BidPriceModel : PageModel
    {
        private readonly IAuctionService _auctionService;
		private readonly IBidService _bidService;
		private readonly IUserService _userService;
		private readonly IInvoiceService _invoiceService;
		private readonly IHubContext<AuctionHub> _hubContext;


		public BidPriceModel(IAuctionService auctionService, IBidService bidService, IUserService userService, IInvoiceService invoiceService, IHubContext<AuctionHub> hubContext)
        {
            _auctionService = auctionService;
            _bidService = bidService;
			_userService = userService;
			_invoiceService = invoiceService;
			_hubContext = hubContext;

		}
        public User LoggedInUser { get; private set; }
		[BindProperty]
		public Auction currentAuction { get;set; }
		[BindProperty]
		[Range(0, double.MaxValue, ErrorMessage = "Bid amount must be a positive number.")]
		public int AmountBid { get; set; }
		[BindProperty]
		public List<Bid> Bids { get; set; } = new List<Bid>();

        public async Task<IActionResult> OnGet(int id)
        {
            await LoadDataAsync(id); 
			if (LoggedInUser==null)
			{
				return RedirectToPage("/Accounts/Login");
			} 
			var user = _userService.GetUserByID(LoggedInUser.Id);
			if (user == null)
			{
				return NotFound();
			}
			LoggedInUser = user;
			if(!user.RoleId.Equals(3) && !user.RoleId.Equals(4) && !user.RoleId.Equals(5))
			{
				return RedirectToPage("/Template/Index");
			}
			if (currentAuction.Status == "End")
			{
				TempData["Message"] = "The auction has ended.";
				return RedirectToPage("/Template/Auctions");
			}
			return Page();

		}

        private async Task LoadDataAsync(int id)
        {
            currentAuction = _auctionService.GetAuctionById(id);
            Bids = _auctionService.GetBidForAuction(id);
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userIdClaim = claimsIdentity?.FindFirst("Id");
            if (userIdClaim == null)
            {
				return;
            }
            LoggedInUser = _userService.GetUserByID(int.Parse(userIdClaim.Value));
		}
		 

		public async Task<IActionResult> OnPost(int auctionId)
		{ 
			await LoadDataAsync(auctionId);
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
			if (!user.RoleId.Equals(3) && !user.RoleId.Equals(4) && !user.RoleId.Equals(5))
			{
				return RedirectToPage("/Accounts/Login");
			}
			if (currentAuction.Status == "End")
			{
				TempData["Message"] = "The auction has ended.";
				return RedirectToPage("/Template/Auctions");
			}
			return Page(); 
		}

		public async Task<IActionResult> OnPostPlaceBid(int auctionId)
		{
			await LoadDataAsync(auctionId);
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
			if (!user.RoleId.Equals(3) && !user.RoleId.Equals(4) && !user.RoleId.Equals(5))
			{
				return RedirectToPage("/Accounts/Login");
			}
			if (currentAuction.Status == "End")
			{
				TempData["Message"] = "The auction has ended.";
				return RedirectToPage("/Template/Auctions");
			}

			// Validating bid amount
			decimal minBidAmount = (Bids != null && Bids.Any()) ? Bids.Max(b => b.Amount) + 50 : currentAuction.Price + 50;

			if (AmountBid < minBidAmount)
			{
				TempData["FailMessage"] = "Bid amount must be at least " + minBidAmount.ToString() +" .";  
				return RedirectToPage("/Auctions/BidPrice", new { id = auctionId }); 
			}
			if (Bids != null && Bids.Any())
			{
				var highestBid = Bids.OrderByDescending(b => b.Amount).FirstOrDefault();

				if (highestBid != null && highestBid.UserId == LoggedInUser.Id)
				{
					TempData["FailMessage"] = "You are already the highest bidder. Please wait for others to bid.";
					return RedirectToPage("/Auctions/BidPrice", new { id = auctionId });
				}
			}


			// Add the new bid
			Bid newBid = new Bid
			{
				UserId = LoggedInUser.Id,  
				Amount = AmountBid,
				AuctionId = currentAuction.Id,
				BidTime = DateTime.Now,
				IsDeleted = false
			};

			_bidService.AddBid(newBid);

			// Notify clients using SignalR
			await _hubContext.Clients.Group(newBid.AuctionId.ToString()).SendAsync("ReceiveNewBid", newBid.AuctionId);
			await _hubContext.Clients.Group(auctionId.ToString()).SendAsync("ReceiveMessageChat", LoggedInUser.Name, "has place highest bid at "+newBid.Amount);



			// Return updated Partial View with the latest bids
			await LoadDataAsync(auctionId);
			TempData["SuccessMessage"] = "Bid placed successfully.";
			return RedirectToPage("/Auctions/BidPrice", new { id = auctionId });
		}


		public async Task<IActionResult> OnPostPauseAuction(int auctionId)
		{
			await LoadDataAsync(auctionId);
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

			if (currentAuction.Status == "End")
			{
				TempData["Message"] = "The auction has ended.";
				return RedirectToPage("/Template/Auctions");
			}
			_auctionService.UpdateAuctionStatus(currentAuction.Id, "Pausing");
			await LoadDataAsync(currentAuction.Id); 
			await _hubContext.Clients.Group(currentAuction.Id.ToString()).SendAsync("ReceiveAuctionStatusUpdate", auctionId, "Pausing");
			return RedirectToPage("/Auctions/BidPrice", new { id = auctionId }); 
		}

		public async Task<IActionResult> OnPostEndAuction(int auctionId)
		{
			await LoadDataAsync(auctionId);
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
			if (currentAuction.Status == "End")
			{
				TempData["Message"] = "The auction has ended.";
				return RedirectToPage("/Template/Auctions");
			} 
			_auctionService.UpdateAuctionStatus(currentAuction.Id, "End"); 

			var bidWithMaxAmount = Bids.OrderByDescending(b => b.Amount).FirstOrDefault();
			if (bidWithMaxAmount != null)
			{
				Invoice invoice = new Invoice { AuctionId = currentAuction.Id, InvoiceDate = DateTime.Now, Status="Pending", IsDelete=false, UserId=bidWithMaxAmount.UserId, TotalPrice=bidWithMaxAmount.Amount};
				_invoiceService.CreateInvoice(invoice);
			}
			await LoadDataAsync(currentAuction.Id);
			await _hubContext.Clients.Group(currentAuction.Id.ToString()).SendAsync("ReceiveAuctionStatusUpdate", auctionId, "End");
			
			return RedirectToPage("/Auctions/BidPrice", new { id = auctionId });

		}

		public async Task<IActionResult> OnPostResumeAuction(int auctionId)
		{
			await LoadDataAsync(auctionId);
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
			if (currentAuction.Status == "End")
			{
				TempData["Message"] = "The auction has ended.";
				return RedirectToPage("/Template/Auctions");
			}
			_auctionService.UpdateAuctionStatus(currentAuction.Id, "Processing");
			await LoadDataAsync(currentAuction.Id);
			await _hubContext.Clients.Group(currentAuction.Id.ToString()).SendAsync("ReceiveAuctionStatusUpdate", auctionId, "Processing");
			return RedirectToPage("/Auctions/BidPrice", new { id = auctionId });

		}
	}
}

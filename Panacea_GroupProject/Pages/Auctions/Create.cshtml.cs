using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using Service;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Panacea_GroupProject.Pages.Auctions
{
	public class CreateModel : PageModel
	{
		private readonly IAuctionService _auctionService;
		private readonly IJewelryService _jewelryService;
		private readonly IUserService _userService;

		public CreateModel(IAuctionService auctionService, IJewelryService jewelryService, IUserService userService)
		{
			_auctionService = auctionService;
			_jewelryService = jewelryService;
			_userService = userService;
		}

		public User LoggedInUser { get; private set; }
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
			if (!LoggedInUser.RoleId.Equals(5))
			{
				return RedirectToPage("/Template/Index");

			}
			ViewData["JewelryId"] = new SelectList(_jewelryService.GetAllJewelries(), "Id", "Name");
			return Page();
		}

		[BindProperty]
		[Required(ErrorMessage = "Please select a Jewelry.")]
		public int JewelryId { get; set; }

		[BindProperty]
		[Required(ErrorMessage = "Please select a Start Date.")]
		public DateTime StartDate { get; set; }

		[BindProperty]
		[Required(ErrorMessage = "Please select an End Date.")]
		public DateTime EndDate { get; set; }

		[BindProperty]
		public string Status { get; set; } = "Pending";

		public IActionResult OnPost()
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
            if (!ModelState.IsValid)
			{
				ViewData["JewelryId"] = new SelectList(_jewelryService.GetAllJewelries(), "Id", "Name");
				return Page();
			}

			// Ensure the selected JewelryId is valid
			var jewelry = _jewelryService.GetJewelryById(JewelryId);
			if (jewelry == null)
			{
				ModelState.AddModelError(string.Empty, "Invalid Jewelry ID.");
				return Page();
			}

			var auctions = _auctionService.GetAuctionByJewelryId(JewelryId);
			if (auctions.Any(a => a.Status == "Processing" || a.Status == "Pending"))
			{
				ModelState.AddModelError(string.Empty, "Cannot create auction for jewelry that already has a 'Processing' or 'Pending' auction.");
				ViewData["JewelryId"] = new SelectList(_jewelryService.GetAllJewelries(), "Id", "Name");
				return Page();
			}

			// Validate StartDate and EndDate
			if (StartDate < DateTime.Today)
			{
				ModelState.AddModelError(nameof(StartDate), "Start Date must be today or a future date.");
				ViewData["JewelryId"] = new SelectList(_jewelryService.GetAllJewelries(), "Id", "Name");
				return Page();
			}
			else if (StartDate >= EndDate)
			{
				ModelState.AddModelError(nameof(EndDate), "End Date must be greater than Start Date.");
				ViewData["JewelryId"] = new SelectList(_jewelryService.GetAllJewelries(), "Id", "Name");
				return Page();
			}


			Auction auction = new Auction
			{
				JewelryId = JewelryId,
				StartDate = StartDate,
				EndDate = EndDate,
				Price = jewelry.Price,
				Status = Status
			};

			// Create the Auction
			_auctionService.CreateAuction(auction);
            ViewData["JewelryId"] = new SelectList(_jewelryService.GetAllJewelries(), "Id", "Name"); 
            TempData["SuccessMessage"] = "Auction created successfully.";
			return Page();
		}
	}
}

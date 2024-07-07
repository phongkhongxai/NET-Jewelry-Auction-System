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

namespace Panacea_GroupProject.Pages.Auctions
{
	public class CreateModel : PageModel
	{
		private readonly IAuctionService _auctionService;
		private readonly IJewelryService _jewelryService;

		public CreateModel(IAuctionService auctionService, IJewelryService jewelryService)
		{
			_auctionService = auctionService;
			_jewelryService = jewelryService;
		}

		public IActionResult OnGet()
		{
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

			TempData["SuccessMessage"] = "Auction created successfully.";
			return RedirectToPage();
		}
	}
}

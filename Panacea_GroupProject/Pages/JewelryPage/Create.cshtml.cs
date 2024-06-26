using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using Service;

namespace Panacea_GroupProject.Pages.JewelryPage
{
	public class CreateModel : PageModel
	{
		private readonly IJewelryService _jewelryService;
		private readonly IAuctionRequestService _auctionRequestService;

		public CreateModel(IJewelryService jewelryService, IAuctionRequestService auctionRequestService)
		{
			_jewelryService = jewelryService;
			_auctionRequestService = auctionRequestService;
		}

        public IActionResult OnGet()
        {
            var auctionRequests = _auctionRequestService.GetAllAuctionRq();
            var selectListItems = auctionRequests.Select(r => new SelectListItem
            {
                Value = r.Id.ToString(), // Value is the Id of the auction request
                Text = r.Title, // Text displayed in the dropdown is the Title of the auction request
            });

            // Prepare data attributes for JavaScript usage
            ViewData["AuctionRequests"] = auctionRequests.Select(r => new
            {
                Id = r.Id,
                Description = r.Description,
                Image = r.Image
            });

            ViewData["AuctionRequestId"] = new SelectList(selectListItems, "Value", "Text");
            return Page();
        }


        [BindProperty]
		public Jewelry Jewelry { get; set; }

		// To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
		public IActionResult OnPost()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			_jewelryService.CreateJewelry(Jewelry);

			return RedirectToPage("./Index");
		}
	}
}

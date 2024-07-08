using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects;
using Service;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;


namespace Panacea_GroupProject.Pages.AuctionRequestPage
{
    public class CreateModel : PageModel
    {
        private readonly IAuctionRequestService _auctionRequestService;
        private readonly IUserService _userService;

		[BindProperty]
		public User LoggedInUser { get; private set; }

		public CreateModel(IAuctionRequestService auctionRequestService, IUserService userService)
        {
           _auctionRequestService = auctionRequestService;
            _userService = userService;
        }

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
            return Page();
		}


        [BindProperty]
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [BindProperty]
        [Url(ErrorMessage = "Invalid URL format")]
        public string Image { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelStateEntry in ModelState.Values)
                {
                    foreach (var error in modelStateEntry.Errors)
                    {
                        Console.WriteLine($"Property: {modelStateEntry}, Error: {error.ErrorMessage}");
                    }
                }
                return Page();
            }
            try
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

				AuctionRequest auction = new AuctionRequest { UserId = LoggedInUser.Id, Title = Title, Description = Description,
                Image = Image,
                    Status = "Pending",
                    RequestDate = DateTime.UtcNow,
                    IsDelete = false
                }; 
                 
                _auctionRequestService.CreateAuctionRequest(auction); 
                TempData["SuccessMessage"] = "Auction request submitted successfully!"; 
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                return RedirectToPage("/Error");
            }
             
        }
    }
}

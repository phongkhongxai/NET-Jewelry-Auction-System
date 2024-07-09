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
using Microsoft.Extensions.Hosting;


namespace Panacea_GroupProject.Pages.AuctionRequestPage
{
    public class CreateModel : PageModel
    {
        private readonly IAuctionRequestService _auctionRequestService;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _environment;


        public User LoggedInUser { get; private set; }

		public CreateModel(IAuctionRequestService auctionRequestService, IUserService userService, IWebHostEnvironment environment)
        {
            _auctionRequestService = auctionRequestService;
            _userService = userService;
            _environment = environment;
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
            if (!LoggedInUser.RoleId.Equals(3))
            {
				return RedirectToPage("/Template/Index");

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
        [Display(Name = "Image")]
        [Required(ErrorMessage = "Image is required.")]
        public IFormFile ImageFile { get; set; }

        public async Task<IActionResult> OnPostAsync()
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
				

                string imageUrl = "default";
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    // Save image to wwwroot/images directory
                    var uploadsDir = Path.Combine(_environment.WebRootPath, "images");
                    if (!Directory.Exists(uploadsDir))
                    {
                        Directory.CreateDirectory(uploadsDir);
                    }

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
                    var filePath = Path.Combine(uploadsDir, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }
                    imageUrl = "/images/" + uniqueFileName;
                }

                AuctionRequest auction = new AuctionRequest { UserId = LoggedInUser.Id, Title = Title, Description = Description,
                Image = imageUrl,
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

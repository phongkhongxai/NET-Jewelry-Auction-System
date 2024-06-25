using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using DataAccessObjects;
using Service;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Panacea_GroupProject.Pages.AuctionRequestPage
{
    public class CreateModel : PageModel
    {
        private readonly IAuctionRequestService _auctionRequestService;
        private readonly IUserService _userService;

        public CreateModel(IAuctionRequestService auctionRequestService, IUserService userService)
        {
           _auctionRequestService = auctionRequestService;
            _userService = userService;
        }

        public IActionResult OnGet()
        {
            var userJson = HttpContext.Session.GetString("LoggedInUser");
            if (string.IsNullOrEmpty(userJson))
            {
                return RedirectToPage("/Accounts/Login");
            }

            User = JsonConvert.DeserializeObject<User>(userJson);
            var user = _userService.GetUserByID(User.Id);
            if (user == null)
            {
                return NotFound();
            }
            User = user;

            return Page();
        }

         
          
        public User User { get; set; }

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
                var userJson = HttpContext.Session.GetString("LoggedInUser");
                if (string.IsNullOrEmpty(userJson))
                {
                    return RedirectToPage("/Accounts/Login");
                }

                User = JsonConvert.DeserializeObject<User>(userJson);
                var user = _userService.GetUserByID(User.Id);
                if (user == null)
                {
                    return NotFound();
                }
                Console.WriteLine(user.Id);

                AuctionRequest auction = new AuctionRequest { User = user, Title = Title, Description = Description,
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

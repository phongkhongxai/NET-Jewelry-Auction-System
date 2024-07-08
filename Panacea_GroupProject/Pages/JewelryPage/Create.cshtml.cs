using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using Service;
using Newtonsoft.Json;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Panacea_GroupProject.Pages.JewelryPage
{
	public class CreateModel : PageModel
	{
		private readonly IJewelryService _jewelryService;
		private readonly IAuctionRequestService _auctionRequestService;
        private readonly IUserService _userService;
		private readonly IMaterialService _materialService;
		private readonly IWebHostEnvironment _environment;


		public User LoggedInUser { get; private set; }

		public CreateModel(IJewelryService jewelryService, IAuctionRequestService auctionRequestService, IUserService userService, IMaterialService material, IWebHostEnvironment environment)
		{
			_jewelryService = jewelryService;
			_auctionRequestService = auctionRequestService;
			_materialService = material;
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
			var auctionRequests = _auctionRequestService.GetAllAuctionRequestsWithoutJewelry();
			ViewData["AuctionRequestId"] = new SelectList(auctionRequests, "Id", "Title");

			var materials = _materialService.GetMaterials();
			var materialOptions = materials.Select(m => new {
				m.Id,
				Name = $"{m.Name} - {m.Price}"
			}).ToList();

			ViewData["MaterialOptions"] = new SelectList(materialOptions, "Id", "Name");
			return Page(); 
        }


         

		[BindProperty]
		[Required(ErrorMessage = "Name is required.")]
		public string Name { get; set; }

		[BindProperty]
		public string Description { get; set; }

		[BindProperty]
		[Required(ErrorMessage = "Price is required.")]
		[Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
		public decimal Price { get; set; }

		[BindProperty]
		[Required(ErrorMessage = "Please select an Auction Request.")]
		public int AuctionRequestId { get; set; }

		[BindProperty]
		[Display(Name = "Image")]
		[Required(ErrorMessage = "Image is required.")]
		public IFormFile ImageFile { get; set; }

		[BindProperty]
		public List<int> SelectedMaterialIds { get; set; } // To store selected material IDs
		[BindProperty]
		public List<decimal> SelectedMaterialQuantities { get; set; } // To store selected material quantities

		  

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
				// If model state is not valid, reload the auction requests and return the page
				var auctionRequests = _auctionRequestService.GetAllAuctionRequestsWithoutJewelry();
                ViewData["AuctionRequestId"] = new SelectList(auctionRequests, "Id", "Title");
				var materials = _materialService.GetMaterials();
				var materialOptions = materials.Select(m => new {
					m.Id,
					Name = $"{m.Name} - {m.Price}" 
				}).ToList();


				ViewData["MaterialOptions"] = new SelectList(materialOptions, "Id", "Name");
				ModelState.AddModelError(string.Empty, "Please select quantities for all materials.");

				return Page();
            }

			if (SelectedMaterialQuantities == null || SelectedMaterialQuantities.Count == 0)
			{
				ModelState.AddModelError(string.Empty, "Please select quantities for all materials.");

				// Reload auction requests and material options for the view
				var auctionRequests = _auctionRequestService.GetAllAuctionRequestsWithoutJewelry();
				ViewData["AuctionRequestId"] = new SelectList(auctionRequests, "Id", "Title");
				var materials = _materialService.GetMaterials();
				var materialOptions = materials.Select(m => new {
					m.Id,
					Name = $"{m.Name} - {m.Price}"
				}).ToList();

				ViewData["MaterialOptions"] = new SelectList(materialOptions, "Id", "Name");
				return Page();
			}

			try
            {
                string imageUrl="default";
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

                // Create Jewelry object and set properties
                Jewelry jewelry = new Jewelry
                {
                    Name = Name,
                    Description = Description,
                    Price = Price,
                    AuctionRequestId = AuctionRequestId,
                    Image = imageUrl   
                };

				if (SelectedMaterialIds != null && SelectedMaterialQuantities != null && SelectedMaterialIds.Count == SelectedMaterialQuantities.Count)
				{
					for (int i = 0; i < SelectedMaterialIds.Count; i++)
					{
						jewelry.JewelryMaterials.Add(new JewelryMaterial { MaterialId = SelectedMaterialIds[i], Quantity = SelectedMaterialQuantities[i] });
					}
				}

				_jewelryService.CreateJewelry(jewelry);
				TempData["SuccessMessage"] = "Created jewlery successfully!";
				return RedirectToPage();
            }
            catch (Exception ex)
            { 
                return RedirectToPage("/Error");
            }
		}
	}
}

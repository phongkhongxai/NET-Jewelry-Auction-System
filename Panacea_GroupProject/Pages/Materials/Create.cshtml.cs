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
using System.Security.Claims;

namespace Panacea_GroupProject.Pages.Materials
{
    public class CreateModel : PageModel
    {
        private readonly IMaterialService _materialService;
        private readonly IUserService _userService;

        public CreateModel(IMaterialService materialService, IUserService userService)
        {
            _materialService = materialService;
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
            if (!LoggedInUser.RoleId.Equals(4) && !LoggedInUser.RoleId.Equals(5))
            {
                return RedirectToPage("/Template/Index");
            }
            return Page();
        }
        public User LoggedInUser { get; private set; }
        [BindProperty]
        public Material Material { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
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
            if (!LoggedInUser.RoleId.Equals(4) && !LoggedInUser.RoleId.Equals(5))
            {
                return RedirectToPage("/Template/Index");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _materialService.CreateMaterial(Material);
            return RedirectToPage("/Materials/ViewMaterials");
        }
    }
}

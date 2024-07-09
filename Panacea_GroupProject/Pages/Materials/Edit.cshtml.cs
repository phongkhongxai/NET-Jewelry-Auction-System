using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using DataAccessObjects;
using Service;
using System.Security.Claims;

namespace Panacea_GroupProject.Pages.Materials
{
    public class EditModel : PageModel
    {
        private readonly IMaterialService _materialService;
        private readonly IUserService _userService;


        public EditModel(IMaterialService materialService, IUserService userService)
        {
            _materialService = materialService;
            _userService = userService;
        }

        [BindProperty]
        public Material Material { get; set; }
        public User LoggedInUser { get; private set; }

        public async Task<IActionResult> OnGetAsync(int id)
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
            Material = _materialService.GetMaterial(id);
            if (Material == null)
            {
                return NotFound();
            }
            return Page();
        }
 
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            { 
                return Page();
            } 
            _materialService.UpdateMaterial(Material); 
            return RedirectToPage("/Materials/Index");
        }

         
    }
}

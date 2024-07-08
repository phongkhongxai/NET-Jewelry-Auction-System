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
using Microsoft.AspNetCore.Authorization;

namespace Panacea_GroupProject.Pages.AdminPage.ManageUser
{
    [Authorize(Policy = "AdminOnly")]
    public class CreateModel : PageModel
    {
        private readonly IUserService _userService;
        public CreateModel(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult OnGet()
        {
        ViewData["RoleId"] = new SelectList(_userService.GetRoles(), "Id", "Name");
            return Page();
        }

        [BindProperty]
        public User UserProfile { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _userService.CreateUser(UserProfile);

            return RedirectToPage("./Index");
        }
    }
}

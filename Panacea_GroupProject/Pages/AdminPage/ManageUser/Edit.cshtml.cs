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
using System.Data;
using Microsoft.AspNetCore.Authorization;

namespace Panacea_GroupProject.Pages.AdminPage.ManageUser
{
    [Authorize(Policy = "AdminOnly")]
    public class EditModel : PageModel
    {
        private readonly IUserService _userService;
        public EditModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public User UserProfile { get; set; }
        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserProfile = _userService.GetUserByID(id.Value);

            if (UserProfile == null)
            {
                return NotFound();
            }

            ViewData["RoleId"] = new SelectList(_userService.GetRoles(), "Id", "Name");

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
                
            _userService.UpdateUser(UserProfile);


            return RedirectToPage("./Index");
        }
    }
}

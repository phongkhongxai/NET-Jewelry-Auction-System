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

namespace Panacea_GroupProject.Pages.AdminPage.ManageUser
{
    public class EditModel : PageModel
    {
        private readonly IUserService _userService;
        public EditModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public User User { get; set; }
        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User = _userService.GetUserByID(id.Value);

            if (User == null)
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
                
            _userService.UpdateUser(User);


            return RedirectToPage("./Index");
        }
    }
}

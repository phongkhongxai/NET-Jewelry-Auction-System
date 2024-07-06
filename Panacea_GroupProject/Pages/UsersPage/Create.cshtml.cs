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

namespace Panacea_GroupProject.Pages.UsersPage
{
    [Authorize(Policy = "AdminOnly")]
    public class CreateModel : PageModel
    {
        //private readonly DataAccessObjects.GroupProjectPRN221 _context;

        //public CreateModel(DataAccessObjects.GroupProjectPRN221 context)
        //{
        //    _context = context;
        //}

        private IUserService userService = new UserService();

		public IActionResult OnGet()
        {
        //ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public User User { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            User.RoleId = 3; //member

            //_context.Users.Add(User);
            //await _context.SaveChangesAsync();

            //userService.CreateUser(User);

			return RedirectToPage("./Index");
        }
    }
}

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

namespace Panacea_GroupProject.Pages.UsersPage
{
    public class EditModel : PageModel
    {
        //private readonly DataAccessObjects.GroupProjectPRN221 _context;

        //public EditModel(DataAccessObjects.GroupProjectPRN221 context)
        //{
        //    _context = context;
        //}

        private readonly IUserService _userService;

        public EditModel(IUserService userService)
        {
            _userService = userService;
        }   

        [BindProperty]
        public User User { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _userService.GetUserByID(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            User = user;
            ViewData["RoleId"] = new SelectList(_userService.GetRoles(), "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                _userService.UpdateUser(User);
            }
            catch (DbUpdateConcurrencyException)
            {
                var user = _userService.GetUserByID(User.Id);
                if (user == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}

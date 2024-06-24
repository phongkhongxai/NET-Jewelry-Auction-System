using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using DataAccessObjects;
using Service;

namespace Panacea_GroupProject.Pages.UsersPage
{
    public class DeleteModel : PageModel
    {
        //private readonly DataAccessObjects.GroupProjectPRN221 _context;

        //public DeleteModel(DataAccessObjects.GroupProjectPRN221 context)
        //{
        //    _context = context;
        //}

        private readonly IUserService _userService;
        public DeleteModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public User User { get; set; } = default!;

        public IActionResult OnGetAsync(int? id)
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
            else
            {
                User = user;
            }
            return Page();
        }

        public IActionResult OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _userService.GetUserByID(id.Value);
            if (user != null)
            {
                user.IsDelete = true;
                _userService.UpdateUser(user);
            }

            return RedirectToPage("./Index");
        }
    }
}

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

namespace Panacea_GroupProject.Pages.AdminPage.ManageUser
{
    public class DeleteModel : PageModel
    {
        private readonly IUserService _userService;
        public DeleteModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public User User { get; set; }

        public IActionResult OnGetAsync(int? id)
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
            return Page();
        }

        public IActionResult OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User = _userService.GetUserByID(id.Value);

            if (User != null)
            {
                User.IsDelete = true;
                _userService.UpdateUser(User);
            }

            return RedirectToPage("./Index");
        }
    }
}

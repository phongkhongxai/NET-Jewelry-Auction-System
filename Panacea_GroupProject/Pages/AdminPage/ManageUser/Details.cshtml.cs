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
using Microsoft.AspNetCore.Authorization;

namespace Panacea_GroupProject.Pages.AdminPage.ManageUser
{
    [Authorize(Policy = "AdminOnly")]
    public class DetailsModel : PageModel
    {
        private readonly IUserService _userService;
        public DetailsModel(IUserService userService)
        {
            _userService = userService;
        }

        public User UserProfile { get; set; }

        public IActionResult OnGetAsync(int? id)
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
            return Page();
        }
    }
}

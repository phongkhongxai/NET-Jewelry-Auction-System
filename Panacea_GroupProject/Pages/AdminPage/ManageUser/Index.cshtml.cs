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
using Panacea_GroupProject.Helpers;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Panacea_GroupProject.Pages.AdminPage.ManageUser
{
    [Authorize(Policy = "AdminOnly")]
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;

        public IndexModel(IUserService userService)
        {
			_userService = userService;
		}

        public IList<User> Users { get; private set; }

        [BindProperty]
        public User LoggedInUser { get; private set; }


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
            Users = _userService.GetUsers();
            return Page();
        }
    }
}

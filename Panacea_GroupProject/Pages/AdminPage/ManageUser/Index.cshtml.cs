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

namespace Panacea_GroupProject.Pages.AdminPage.ManageUser
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;

        public IndexModel(IUserService userService)
        {
			_userService = userService;
		}

        public IList<User> Users { get; private set; }
        public User LoggedInUser { get; private set; }


        public IActionResult OnGet()
        {
            LoggedInUser = HttpContext.Session.GetObjectFromJson<User>("LoggedInUser");
            Users = _userService.GetUsers();
            return Page();
        }
    }
}

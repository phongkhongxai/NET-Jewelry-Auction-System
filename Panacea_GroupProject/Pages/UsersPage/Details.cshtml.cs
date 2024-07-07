using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using DataAccessObjects;
using Microsoft.AspNetCore.Authorization;
using Service;

namespace Panacea_GroupProject.Pages.UsersPage
{
    [Authorize(Policy = "AdminOnly")]
    public class DetailsModel : PageModel
    {
        //private readonly DataAccessObjects.GroupProjectPRN221 _context;

        //public DetailsModel(DataAccessObjects.GroupProjectPRN221 context)
        //{
        //    _context = context;
        //}

        private readonly IUserService _userService;

        public DetailsModel(IUserService userService)
        {
            _userService = userService;
        }

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
            else
            {
                User = user;
            }
            return Page();
        }
    }
}

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

namespace Panacea_GroupProject.Pages.UsersPage
{
    public class IndexModel : PageModel
    {
        //private readonly DataAccessObjects.GroupProjectPRN221 _context;

        //public IndexModel(DataAccessObjects.GroupProjectPRN221 context)
        //{
        //    _context = context;
        //}

        private readonly IUserService _userService;
        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        public IList<User> Users { get;set; } = default!;

        public void OnGet()
        {
            //User = await _context.Users
            //    .Include(u => u.Role).ToListAsync();
            Users = _userService.GetUsers();
        }
    }
}

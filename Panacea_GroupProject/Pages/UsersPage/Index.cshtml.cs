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
    public class IndexModel : PageModel
    {
        //private readonly DataAccessObjects.GroupProjectPRN221 _context;

        //public IndexModel(DataAccessObjects.GroupProjectPRN221 context)
        //{
        //    _context = context;
        //}

        private IUserService userService = new UserService();

        public IList<User> User { get;set; } = default!;

        public async Task OnGetAsync()
        {
            //User = await _context.Users
            //    .Include(u => u.Role).ToListAsync();
            User = userService.GetUsers();
        }
    }
}

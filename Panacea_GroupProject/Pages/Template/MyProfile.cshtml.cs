using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Panacea_GroupProject.Helpers;
using Service;

namespace Panacea_GroupProject.Pages.Template
{
    public class MyProfileModel : PageModel
    {
        private readonly IUserService _userService;
        public MyProfileModel(IUserService userService)
        {
            _userService = userService;
        }

        public User User { get; set; } = default!;
        public IActionResult OnGet()
        {
            User = HttpContext.Session.GetObjectFromJson<User>("LoggedInUser");
            if (User == null)
            {
                return RedirectToPage("/Accounts/Login");
            }

            return Page();
        }
    }
}

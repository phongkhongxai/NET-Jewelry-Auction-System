using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Panacea_GroupProject.Helpers;
using Service;

namespace Panacea_GroupProject.Pages.Template
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;

        public User LoggedInUser { get; private set; }
        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        public void OnGet()
        {
            LoggedInUser = HttpContext.Session.GetObjectFromJson<User>("LoggedInUser");
        }
    }
}

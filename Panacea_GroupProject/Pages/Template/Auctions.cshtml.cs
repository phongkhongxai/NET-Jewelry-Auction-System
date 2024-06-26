using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Panacea_GroupProject.Helpers;
using Service;

namespace Panacea_GroupProject.Pages.Template
{
    public class AuctionsModel : PageModel
    {
        private readonly IUserService _userService;
        public AuctionsModel(IUserService userService)
        {
            _userService = userService;
        }

        public User LoggedInUser { get; private set; }
        public void OnGet()
        {
            LoggedInUser = HttpContext.Session.GetObjectFromJson<User>("LoggedInUser");
        }
    }
}

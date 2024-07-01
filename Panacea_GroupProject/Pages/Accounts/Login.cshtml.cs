using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Panacea_GroupProject.Helpers;
using Service;

namespace Panacea_GroupProject.Pages.Accounts
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        
        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult OnPost()
        {
            var user = _userService.ValidateUser(Email, Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return Page();
            }
            HttpContext.Session.SetObjectAsJson("LoggedInUser", user);   
            if(user.RoleId.Equals(1))
            {
                return RedirectToPage("/AdminPage/ManageUser/Index");
            } else
            {
                return RedirectToPage("/Template/Index"); 
            }
        }
    }
}

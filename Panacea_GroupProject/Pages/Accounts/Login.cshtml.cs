using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Panacea_GroupProject.Helpers;
using Service;
using System.Security.Claims;

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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var user = _userService.ValidateUser(Email, Password);

            if (user == null)
            {
                ModelState.AddModelError(nameof(Password), "Invalid email or password.");
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Name,  user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("RoleId", user.RoleId.ToString()),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };
            var identity = new ClaimsIdentity(claims, "CookieAuth");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("CookieAuth", claimsPrincipal);
            
            if(user.RoleId.Equals(1))
            {
                return RedirectToPage("/AdminPage/AdminDashboard");
            }

            return RedirectToPage("/Template/Index");
        }
    }
}

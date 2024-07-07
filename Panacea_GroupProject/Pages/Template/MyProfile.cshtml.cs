using BusinessObjects;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Panacea_GroupProject.Helpers;
using Service;
using System.Security.Claims;

namespace Panacea_GroupProject.Pages.Template
{
    public class MyProfileModel : PageModel
    {
        private readonly IUserService _userService;
        public MyProfileModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public User UserProfile { get; set; } = default!;
        public IActionResult OnGet()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userIdClaim = claimsIdentity?.FindFirst("Id");
            if (userIdClaim == null)
            {
                return RedirectToPage("/Accounts/Login");
            }

            UserProfile = _userService.GetUserByID(int.Parse(userIdClaim.Value));

            if (UserProfile == null)
            {
                return RedirectToPage("/Accounts/Login");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userIdClaim = claimsIdentity?.FindFirst("Id");

            if (userIdClaim == null)
            {
                return RedirectToPage("/Accounts/Login");
            }

            UserProfile = _userService.GetUserByID(int.Parse(userIdClaim.Value));

            if (UserProfile == null)
            {
                return RedirectToPage("/Accounts/Login");
            }

            // Update user properties with form data
            UserProfile.Name = UserProfile.Name;
            UserProfile.Email = UserProfile.Email;
            UserProfile.Address = UserProfile.Address;
            UserProfile.Dob = UserProfile.Dob;

            // Update user in the database
            _userService.UpdateUser(UserProfile);

            // Update the claims and re-issue the cookie
            var claims = new List<Claim>
            {
                new Claim("Id", UserProfile.Id.ToString()), 
                new Claim(ClaimTypes.Name, UserProfile.Name),
                new Claim(ClaimTypes.Email, UserProfile.Email),
                new Claim("RoleId", UserProfile.RoleId.ToString()),
                new Claim(ClaimTypes.Role, UserProfile.Role.Name)
            };

            var identity = new ClaimsIdentity(claims, "CookieAuth");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("CookieAuth", principal);

            TempData["SuccessMessage"] = "Profile updated successfully!";

            return RedirectToPage("/Template/MyProfile");
        }
    }
}

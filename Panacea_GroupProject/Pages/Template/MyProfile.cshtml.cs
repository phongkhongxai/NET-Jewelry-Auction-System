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

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            User = HttpContext.Session.GetObjectFromJson<User>("LoggedInUser");
            if (User == null)
            {
                return RedirectToPage("/Accounts/Login");
            }

            // Update user properties with form data
            User.Name = Request.Form["name"];
            User.Email = Request.Form["email"];
            User.Address = Request.Form["address"];
            User.Dob = DateOnly.Parse(Request.Form["dob"]);

            // Update user in the database
            _userService.UpdateUser(User);

            // Update the session
            HttpContext.Session.SetObjectAsJson("LoggedInUser", User);

            TempData["SuccessMessage"] = "Profile updated successfully!";

            return RedirectToPage("/Template/MyProfile");
        }
    }
}

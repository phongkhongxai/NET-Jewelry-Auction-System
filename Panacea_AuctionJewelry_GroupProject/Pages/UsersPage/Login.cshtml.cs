using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;

namespace Panacea_AuctionJewelry_GroupProject.Pages.UsersPage
{
    public class LoginModel : PageModel
    {
        private readonly UserService userService;

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public LoginModel()
        {
            userService = new UserService();
        }


        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            var user = userService.ValidateUser(Email, Password);

            if (user == null)
            {
                ErrorMessage = "Invalid email or password";
                return Page();
            }

            if (user.RoleId == 1)
            {
                // Người dùng có vai trò "Admin"
                // Thực hiện hành động tương ứng
                return RedirectToPage("/UsersPage/AdminPage");
            }
            else if (user.RoleId == 4)
            {
                // Người dùng có vai trò "Staff"
                // Thực hiện hành động tương ứng
                return RedirectToPage("/Staff/Index");
            }
            else
            {
                // Người dùng không thuộc bất kỳ vai trò nào
                // Thực hiện hành động mặc định
                return RedirectToPage("/Index");
            }
        }
    }
}

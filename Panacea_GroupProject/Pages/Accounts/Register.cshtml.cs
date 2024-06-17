using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using System.ComponentModel.DataAnnotations;

namespace Panacea_GroupProject.Pages.Accounts
{
    public class RegisterModel : PageModel
    {
        private readonly IUserService _userService;

  

        public RegisterModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [BindProperty]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [BindProperty]
        [MaxLength(15)]
        public string Phone { get; set; }

        [BindProperty]
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }

        [BindProperty]
        [MaxLength(10)] 
        [RegularExpression("^(Male|Female)$", ErrorMessage = "Gender must be Male or Female.")]
        public string Gender { get; set; }

        [BindProperty]
        [MaxLength(200)]
        public string Address { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var newUser = new User
            {
                Name = Name,
                Email = Email,
                Password = Password,
                Phone = Phone,
                Dob = DateOnly.FromDateTime(Dob),
                Gender = Gender,
                Address = Address,
                RoleId = 3  
            };

            try
            {
                _userService.CreateUser(newUser);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Email or Phone already exist.");
                return Page();
            }

            return RedirectToPage("/Accounts/Login");
        }
    }
}

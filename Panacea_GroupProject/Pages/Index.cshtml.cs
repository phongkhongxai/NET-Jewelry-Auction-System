using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using System.Security.Claims;

namespace Panacea_GroupProject.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		private readonly IUserService _userService;

        [BindProperty]
        public User LoggedInUser { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, IUserService userService)
		{
			_logger = logger;
			_userService = userService;
		}

		public IActionResult OnGet()
		{
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userIdClaim = claimsIdentity?.FindFirst("Id");
            if (userIdClaim == null)
            {
                return Page();
            }

            LoggedInUser = _userService.GetUserByID(int.Parse(userIdClaim.Value));

            if (LoggedInUser == null)
            {
                return Page();
            }

            return Page();
        }
	}
}

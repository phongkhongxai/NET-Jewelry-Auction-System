using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using System.Security.Claims;

namespace Panacea_GroupProject.Pages.Materials
{
    public class ViewMaterialsModel : PageModel
    {
        private readonly IMaterialService _materialService;
        private readonly IUserService _userService;
        public ViewMaterialsModel(IMaterialService materialService, IUserService userService)
        {
            _materialService = materialService;
            _userService = userService;
        }
        public User LoggedInUser { get; private set; }
        public IList<Material> Material { get; set; }

        public IActionResult OnGet()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userIdClaim = claimsIdentity?.FindFirst("Id");
            if (userIdClaim == null)
            {
                return RedirectToPage("/Accounts/Login");
            }
            LoggedInUser = _userService.GetUserByID(int.Parse(userIdClaim.Value));
            if (LoggedInUser == null)
            {
                return RedirectToPage("/Accounts/Login");
            }
            if (!LoggedInUser.RoleId.Equals(4) && !LoggedInUser.RoleId.Equals(5))
            {
                return RedirectToPage("/Template/Index");
            }
            Material = _materialService.GetMaterials();
            return Page();
        }
    }
}

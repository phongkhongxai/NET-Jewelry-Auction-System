using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Panacea_AuctionJewelry_GroupProject.Pages.UsersPage
{
    [Authorize(Roles = "Admin")]
    public class AdminPageModel : PageModel
    {
        public void OnGet()
        {
            // Code logic for GET request
        }

        public AdminPageModel() { }
    }
}

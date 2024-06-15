using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Panacea_GroupProject.Pages.Accounts
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnPost()
        {
            HttpContext.Session.Remove("LoggedInUser"); 
            return RedirectToPage("/Index");  
        }
    }
}

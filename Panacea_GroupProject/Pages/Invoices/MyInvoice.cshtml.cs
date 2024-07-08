using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using System.Security.Claims;

namespace Panacea_GroupProject.Pages.Invoices
{
    public class MyInvoiceModel : PageModel
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IUserService _userService;

        public MyInvoiceModel(IInvoiceService invoiceService, IUserService userService)
        {
            _invoiceService = invoiceService;
            _userService = userService;
        }
        public User LoggedInUser { get; private set; }
        public IList<Invoice> Invoice { get; set; }
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
            if (!LoggedInUser.RoleId.Equals(3))
            {
                return RedirectToPage("/Template/Index");
            }
            Invoice = _invoiceService.GetInvoiceByUserId(LoggedInUser.Id);
            return Page();
        }
    }
}

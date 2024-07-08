using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using DataAccessObjects;
using Service;
using System.Security.Claims;

namespace Panacea_GroupProject.Pages.Invoices
{
    public class ViewInvoiceModel : PageModel
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IUserService _userService;

        public ViewInvoiceModel(IInvoiceService invoiceService, IUserService userService)
        {
            _invoiceService = invoiceService;
            _userService = userService;
        }
        [BindProperty]
        public User LoggedInUser { get; private set; }
        public Invoice Invoice { get;set; }

        public IActionResult OnGet(int id)
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
            if (LoggedInUser != null)
            {
                Invoice = _invoiceService.GetInvoiceByUserId(LoggedInUser.Id);
            }
            return Page();
        }
    }
}

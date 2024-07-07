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

namespace Panacea_GroupProject.Pages.Invoices
{
    public class ViewInvoiceModel : PageModel
    {
        private readonly IInvoiceService _invoiceService;

        public ViewInvoiceModel(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        public Invoice Invoice { get;set; }
        public User User { get; set; }

        public IActionResult OnGet(int id)
        {
            Invoice = _invoiceService.GetInvoiceById(id);
            if (Invoice == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

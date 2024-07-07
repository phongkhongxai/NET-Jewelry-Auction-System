using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using DataAccessObjects;

namespace Panacea_GroupProject.Pages.Invoices
{
    public class ViewInvoiceModel : PageModel
    {
        private readonly DataAccessObjects.GroupProjectPRN221 _context;

        public ViewInvoiceModel(DataAccessObjects.GroupProjectPRN221 context)
        {
            _context = context;
        }

        public IList<Invoice> Invoice { get;set; }

        public async Task OnGetAsync()
        {
            Invoice = await _context.Invoices
                .Include(i => i.Auction)
                .Include(i => i.User).ToListAsync();
        }
    }
}

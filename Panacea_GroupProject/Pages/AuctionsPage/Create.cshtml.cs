using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using DataAccessObjects;

namespace Panacea_GroupProject.Pages.AuctionsPage
{
    public class CreateModel : PageModel
    {
        private readonly DataAccessObjects.GroupProjectPRN221 _context;

        public CreateModel(DataAccessObjects.GroupProjectPRN221 context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["JewelryId"] = new SelectList(_context.Jewelries, "Id", "Description");
            return Page();
        }

        [BindProperty]
        public Auction Auction { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Auctions.Add(Auction);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

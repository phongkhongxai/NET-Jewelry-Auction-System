using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using DataAccessObjects;
using Service;

namespace Panacea_GroupProject.Pages.AuctionsPage
{
    public class EditModel : PageModel
    {
        private readonly IAuctionService _auctionService;
        public EditModel(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        [BindProperty]
        public Auction Auction { get; set; }

        public IActionResult OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Auction = _auctionService.GetAuctionById(id.Value);

            if (Auction == null)
            {
                return NotFound();
            }
           ViewData["JewelryId"] = new SelectList(_auctionService.GetAllAuctions(), "Id", "Description");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                _auctionService.UpdateAuction(Auction);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuctionExists(Auction.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AuctionExists(int id)
        {
            return _auctionService.GetAuctionById(id) != null;
        }
    }
}

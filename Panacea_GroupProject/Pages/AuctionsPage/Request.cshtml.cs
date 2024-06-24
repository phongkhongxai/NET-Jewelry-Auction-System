using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects;

namespace Panacea_GroupProject.Pages.AuctionsPage
{
    public class RequestModel : PageModel
    {
        [BindProperty]
        public AuctionRequest AuctionsRequest { get; set; } = default!;
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Add logic to save the AuctionsRequest to the database
            // e.g., _context.AuctionRequests.Add(AuctionsRequest);
            // _context.SaveChanges();

            return RedirectToPage("/AuctionsPage/Success");
        }
    }
}

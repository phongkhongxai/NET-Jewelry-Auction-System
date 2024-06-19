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

namespace Panacea_GroupProject.Pages.AuctionsPage
{
    public class IndexModel : PageModel
    {
        private readonly IAuctionService _auctionService;

        public IndexModel(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        public IList<Auction> Auction { get;set; }

        public void OnGetAsync()
        {
            Auction = _auctionService.GetAllAuctions();
        }
    }
}

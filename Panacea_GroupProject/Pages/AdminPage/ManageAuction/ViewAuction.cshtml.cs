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

namespace Panacea_GroupProject.Pages.AdminPage.ManageAuction
{
    public class ViewAuctionModel : PageModel
    {
        private readonly IAuctionService _auctionService;
        public ViewAuctionModel(IAuctionService auctionService)
        {
			_auctionService = auctionService;
		}

        public IList<Auction> Auction { get;set; }

        public void OnGet(string status)
        {
            var allauctions = _auctionService.GetAllAuctions();
            DateTime now = DateTime.Now;

            //ongoing auctions, upcoming auctions, and compleled
            Auction = status switch
            {
                "Ongoing" => allauctions.Where(a => a.StartDate <= now && a.EndDate >= now).ToList(),
                "Upcoming" => allauctions.Where(a => a.StartDate > now).ToList(),
                "Completed" => allauctions.Where(a => a.EndDate < now).ToList(),
                _ => allauctions.ToList()
            };
        }
    }
}

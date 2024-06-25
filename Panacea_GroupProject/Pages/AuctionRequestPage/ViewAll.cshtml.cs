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

namespace Panacea_GroupProject.Pages.AuctionRequestPage
{
    public class ViewAllModel : PageModel
    {  
        private readonly IAuctionRequestService _auctionRequestService;
        private readonly IUserService _userService;

        public ViewAllModel(IAuctionRequestService auctionRequestService, IUserService userService)
        {
            _auctionRequestService = auctionRequestService;
            _userService = userService;
        }

        public IList<AuctionRequest> AuctionRequest { get;set; }

        public async Task OnGetAsync()
        {
           
        }
    }
}

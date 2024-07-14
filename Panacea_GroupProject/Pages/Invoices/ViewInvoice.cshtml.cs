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
using Microsoft.AspNetCore.SignalR;

namespace Panacea_GroupProject.Pages.Invoices
{
	public class ViewInvoiceModel : PageModel
	{
		private readonly IInvoiceService _invoiceService;
		private readonly IUserService _userService;
		private readonly IAuctionService _auctionService;
		private readonly IJewelryService _jewelryService;

		public ViewInvoiceModel(IInvoiceService invoiceService, IUserService userService, IAuctionService auctionService, IJewelryService jewelryService)
		{
			_invoiceService = invoiceService;
			_userService = userService;
			_auctionService = auctionService;
			_jewelryService = jewelryService;
		}

		public User LoggedInUser { get; private set; }
		public Invoice Invoice { get; set; }

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
			Invoice = _invoiceService.GetInvoiceById(id);

			if (!LoggedInUser.RoleId.Equals(4) && !LoggedInUser.RoleId.Equals(5) && !LoggedInUser.RoleId.Equals(3))
			{
				return RedirectToPage("/Template/Index");
			}
			else if(LoggedInUser.RoleId.Equals(3) && Invoice.UserId == LoggedInUser.Id)
			{
				return Page();
			} 
			else if(LoggedInUser.RoleId.Equals(4) || LoggedInUser.RoleId.Equals(5))
			{
				return Page();
			} 
			else
			{
                return RedirectToPage("/Template/Index");
            }
			 
		}

		public IActionResult OnPost(int id)
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

			if(!LoggedInUser.RoleId.Equals(4) && !LoggedInUser.RoleId.Equals(5))
			{ 
				return RedirectToPage("/Template/Index");
			} 

			return Page();
		}

		public async Task<IActionResult> OnPostComplete(int invoiceId)
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

			if (!LoggedInUser.RoleId.Equals(4) && !LoggedInUser.RoleId.Equals(5))
			{
				return RedirectToPage("/Template/Index");
			}

			Invoice = _invoiceService.GetInvoiceById(invoiceId);
			Invoice.Status = "Paid";
			_invoiceService.UpdateInvoice(Invoice);
            Jewelry jewelry = _jewelryService.GetJewelryById(Invoice.Auction.JewelryId);
            if (jewelry != null)
            {
                _jewelryService.DeleteJewelry(jewelry);
            }

            return Page();

		}

		public async Task<IActionResult> OnPostReject(int invoiceId)
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

			if (!LoggedInUser.RoleId.Equals(4) && !LoggedInUser.RoleId.Equals(5))
			{
				return RedirectToPage("/Template/Index");
			} 

			Invoice = _invoiceService.GetInvoiceById(invoiceId);
			Invoice.Status = "Reject";
			_invoiceService.UpdateInvoice(Invoice);
			Auction auction = _auctionService.GetAuctionById(Invoice.AuctionId);
			if (auction != null)
			{ 
				_auctionService.UpdateAuctionStatus(auction.Id, "Fail");
			}
			return Page();

		}
	}
}

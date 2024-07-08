using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Service;
using System.Security.AccessControl;

namespace Panacea_GroupProject.Pages.AdminPage
{
    public class AdminDashboardModel : PageModel
    {
        private readonly IAuctionService _auctionService;
        private readonly IUserService _userService;
        private readonly IJewelryService _jewelryService;

        public AdminDashboardModel(IAuctionService auctionService, IUserService userService, IJewelryService jewelryService)
        {
            _auctionService = auctionService;
            _userService = userService;
            _jewelryService = jewelryService;
        }

        public IList<object> ListObject { get; set; }
        public string ObjectType { get; set; }

        public void OnGet(string objectName)
        {
            if (!string.IsNullOrEmpty(objectName))
            {
                ObjectType = objectName;
                if (objectName.Equals("Auction", StringComparison.OrdinalIgnoreCase))
                {
                    ListObject = _auctionService.GetAllAuctions().Cast<object>().ToList();
                }
                else if (objectName.Equals("User", StringComparison.OrdinalIgnoreCase))
                {
                    ListObject = _userService.GetUsers().Cast<object>().ToList();
                }
                else if (objectName.Equals("Jewelry", StringComparison.OrdinalIgnoreCase))
                {
                    ListObject = _jewelryService.GetAllJewelries().Cast<object>().ToList();
                }
            }
        }

        public IActionResult OnPostEdit(string objectName)
        {
            switch (objectName)
            {
                case "Auction":
                    var Id = Request.Form["Id"];
                    Auction a = _auctionService.GetAuctionById(int.Parse(Id));
                    var StartDate = DateTime.Parse(Request.Form["StartDate"]);
                    var EndDate = DateTime.Parse(Request.Form["EndDate"]);
                    if (a != null)
                    {
                        a.StartDate = StartDate;
                        a.EndDate = EndDate;
                        _auctionService.UpdateAuction(a);
                        return Page();
                    }
                    break;
                case "User":
                    var Id_u = Request.Form["Id"];
                    User u = _userService.GetUserByID(int.Parse(Id_u));
                    var Name = Request.Form["Name"];
                    var Email = Request.Form["Email"];
                    var Phone = Request.Form["Phone"];
                    var Address = Request.Form["Address"];
                    if (u != null)
                    {
                        u.Name = Name;
                        u.Email = Email;
                        u.Phone = Phone;
                        u.Address = Address;
                        _userService.UpdateUser(u);
                        return Page();
                    }
                    break;
                case "Jewelry":
                    var Id_j = Request.Form["Id"];
                    Jewelry j = _jewelryService.GetJewelryById(int.Parse(Id_j));
                    var Name_J = Request.Form["Name"];
                    var Price = decimal.Parse(Request.Form["Price"]);
                    if (j != null)
                    {
                        j.Name = Name_J;
                        j.Price = Price;
                        _jewelryService.UpdateJewelry(j);
                        return Page();
                    }
                    break;
            }
            return Page();
        }


        public IActionResult OnPostDelete(string objectName, int id)
        {

            switch (objectName)
            {
                case "Auction":
                    var auction = _auctionService.GetAuctionById(id);
                    if (auction != null)
                    {
                        _auctionService.DeleteAuction(auction);
                    }
                    break;
                case "User":
                    var user = _userService.GetUserByID(id);
                    if (user != null)
                    {
                        _userService.DeleteUser(user);
                    }
                    break;
                case "Jewelry":
                    var jewelry = _jewelryService.GetJewelryById(id);
                    if (jewelry != null)
                    {
                        _jewelryService.DeleteJewelry(jewelry);
                    }
                    break;
            }
            return Page();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;

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
    }
}

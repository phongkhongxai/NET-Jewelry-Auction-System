using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;

namespace Panacea_GroupProject.Pages.Template
{
    public class JewelleryModel : PageModel
    {
        private readonly IJewelryService _jewelryService;
        public JewelleryModel(IJewelryService jewelryService)
        {
            _jewelryService = jewelryService;
        }
        public List<Jewelry> Jewelries { get; set; }
        public void OnGet()
        {
            Jewelries = _jewelryService.GetAllJewelries();
        }
    }
}

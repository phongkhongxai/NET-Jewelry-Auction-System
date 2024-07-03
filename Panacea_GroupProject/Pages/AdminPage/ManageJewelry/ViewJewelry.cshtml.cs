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

namespace Panacea_GroupProject.Pages.AdminPage.ManageJewelry
{
    public class ViewJewelryModel : PageModel
    {
        private readonly IJewelryService _jewelryService;
        public ViewJewelryModel(IJewelryService jewelryService)
        {
            _jewelryService = jewelryService;
        }

        public IList<Jewelry> Jewelry { get;set; }
        public int PageSize { get; } = 10; // Number of items per page
        public int CurrentPage { get; set; } = 1; // Current page, default to page 1
        public int TotalItems { get; set; } // Total number of items

        private void LoadJewelry()
        {
            Jewelry = _jewelryService.GetJewelriesByPage(CurrentPage, PageSize);
            TotalItems = _jewelryService.GetTotalJewelries();
        }

        public void OnGet()
        {
            LoadJewelry();
        }

        public void OnGetPage(int page)
        {
            CurrentPage = page;
            LoadJewelry();
        }
    }
}

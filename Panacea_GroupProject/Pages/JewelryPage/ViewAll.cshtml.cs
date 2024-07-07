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

namespace Panacea_GroupProject.Pages.JewelryPage
{
    public class ViewAllModel : PageModel
    {
        private readonly IJewelryService _jewelryService;
        public ViewAllModel(IJewelryService jewelryService)
        {
            _jewelryService = jewelryService;
        }

        public IList<Jewelry> Jewelry { get;set; }

        public void OnGet()
        {
            Jewelry = _jewelryService.GetAllJewelries();
        }
    }
}

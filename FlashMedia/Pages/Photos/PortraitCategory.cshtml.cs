using FlashMedia.Data;
using FlashMedia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlashMedia.Pages.Photos
{
    public class PortraitCategoryModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public PortraitCategoryModel(ILogger<PortraitCategoryModel> logger, ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Photo> PortraitPhotos { get; set; }
        public void OnGet()
        {
            PortraitPhotos = _context.Photos
                .Where(p => p.CategoryId == 2)
                .OrderByDescending(p => p.Date)
                .Take(5)
                .ToList();
        }
    }
}

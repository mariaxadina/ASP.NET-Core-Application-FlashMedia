using FlashMedia.Data;
using FlashMedia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlashMedia.Pages.Photos
{
    public class LandscapeCategoryModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public LandscapeCategoryModel(ILogger<LandscapeCategoryModel> logger, ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Photo> LandscapePhotos { get; set; }
        public void OnGet()
        {
            LandscapePhotos = _context.Photos
                .Where(p => p.CategoryId == 4)
                .OrderByDescending(p => p.Date)
                .Take(5)
                .ToList();
        }
    }
}

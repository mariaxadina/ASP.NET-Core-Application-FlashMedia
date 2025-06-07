using FlashMedia.Data;
using FlashMedia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlashMedia.Pages.Photos
{
    public class NatureCategoryModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public NatureCategoryModel(ILogger<NatureCategoryModel> logger, ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Photo> NaturePhotos { get; set; }
        public void OnGet()
        {
            NaturePhotos = _context.Photos
                .Where(p => p.CategoryId == 1)
                .OrderByDescending(p => p.Date)
                .Take(5)
                .ToList();
        }
    }
}

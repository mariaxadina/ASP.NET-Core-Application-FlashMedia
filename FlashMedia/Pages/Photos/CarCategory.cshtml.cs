using FlashMedia.Data;
using FlashMedia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlashMedia.Pages.Photos
{
    public class CarCategoryModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public CarCategoryModel(ILogger<CarCategoryModel> logger, ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Photo> CarPhotos { get; set; }
        public void OnGet()
        {
            CarPhotos = _context.Photos
                .Where(p => p.CategoryId == 3) 
                .OrderByDescending(p => p.Date)
                .Take(5)
                .ToList();
        }
    }
}

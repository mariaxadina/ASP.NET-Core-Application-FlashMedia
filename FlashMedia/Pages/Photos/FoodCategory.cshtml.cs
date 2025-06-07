using FlashMedia.Data;
using FlashMedia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlashMedia.Pages.Photos
{
    public class FoodCategoryModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public FoodCategoryModel(ILogger<FoodCategoryModel> logger, ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Photo> FoodPhotos { get; set; }
        public void OnGet()
        {
            FoodPhotos = _context.Photos
                .Where(p => p.CategoryId == 6)
                .OrderByDescending(p => p.Date)
                .Take(5)
                .ToList();
        }
    }
}
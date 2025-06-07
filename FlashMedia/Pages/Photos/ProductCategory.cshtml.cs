using FlashMedia.Data;
using FlashMedia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlashMedia.Pages.Photos
{
    public class ProductCategoryModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public ProductCategoryModel(ILogger<ProductCategoryModel> logger, ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Photo> ProductPhotos { get; set; }
        public void OnGet()
        {
            ProductPhotos = _context.Photos
                .Where(p => p.CategoryId == 5)
                .OrderByDescending(p => p.Date)
                .Take(5)
                .ToList();
        }
    }
}

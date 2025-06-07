using FlashMedia.Data;
using FlashMedia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FlashMedia.Pages.Map
{
    public class MapModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public MapModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Photo> NearbyPhotos { get; set; }

        public async Task<IActionResult> OnGetAsync(double? lat, double? lon)
        {
            if (lat.HasValue && lon.HasValue)
            {
                const double maxDistanceKm = 1000; // adjust as needed
                double latRange = maxDistanceKm / 111.0; // ~111 km per latitude degree
                double lonRange = maxDistanceKm / (111.0 * Math.Cos(lat.Value * Math.PI / 180.0));

                NearbyPhotos = await _context.Photos
                    .Where(p =>
                        Math.Abs(p.Latitude - lat.Value) <= latRange &&
                        Math.Abs(p.Longitude - lon.Value) <= lonRange)
                    .ToListAsync();
            }

            return Page();
        }
    }
}

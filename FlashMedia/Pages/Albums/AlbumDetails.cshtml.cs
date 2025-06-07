using FlashMedia.Data;
using FlashMedia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FlashMedia.Pages.Albums
{
    public class AlbumDetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AlbumDetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Album Album { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Album = await _context.Albums
                .Include(a => a.Photos)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (Album == null)
                return NotFound();

            return Page();
        }

    }
}

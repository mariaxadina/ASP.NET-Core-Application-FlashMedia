using FlashMedia.Data;
using FlashMedia.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

public class ProfileModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public ProfileModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public List<Album> UserAlbums { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "You must add a name before adding the album.")]
    public string NewAlbumName { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var userId = _userManager.GetUserId(User);
        UserAlbums = await _context.Albums
            .Where(a => a.UserId == userId)
            .ToListAsync();

        return Page();
    }

    public async Task<IActionResult> OnPostAddAlbumAsync()
    {
        if (string.IsNullOrWhiteSpace(NewAlbumName))
        {
            ModelState.AddModelError("NewAlbumName", "You must add a name before adding the album.");
            await OnGetAsync(); // Re-populate UserAlbums for redisplay
            return Page();
        }

        var userId = _userManager.GetUserId(User);

        var album = new Album
        {
            Title = NewAlbumName,
            UserId = userId
        };

        _context.Albums.Add(album);
        await _context.SaveChangesAsync();

        return RedirectToPage();
    }
}

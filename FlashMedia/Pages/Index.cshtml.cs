using FlashMedia.Data;
using FlashMedia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FlashMedia.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ApplicationDbContext _context;


    public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }



    public List<Photo> LatestPhotos { get; set; }

    public List<Photo> ResultPhotos { get; set; }

    [BindProperty(SupportsGet = true)]
    public string SearchTerm { get; set; }
    public void OnGet()
    {
        LatestPhotos = _context.Photos
            .OrderByDescending(p => p.Date)
            .Take(5)
            .ToList();

        if (!string.IsNullOrEmpty(SearchTerm))
        {
            ResultPhotos = _context.Photos
                .Where(p => p.Name.Contains(SearchTerm))
                .OrderByDescending(p => p.Date)
                .ToList();
        }
        else
        {
            ResultPhotos = new List<Photo>(); 
        }
    }
}
using FlashMedia.Data;
using FlashMedia.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Azure;
using Humanizer;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FlashMedia.Pages.Photos
{
    [Authorize]
    public class AddPhotoModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly UserManager<IdentityUser> _userManager;

        public AddPhotoModel(ApplicationDbContext context, IWebHostEnvironment environment, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _environment = environment;
            _userManager = userManager;
        }

        [BindProperty]
        public AddPhoto Photo { get; set; }

        public List<SelectListItem> Categories { get; set; }

        [BindProperty]
        public IFormFile UploadedFile { get; set; }

        [BindProperty] public string Latitude { get; set; }

        [BindProperty] public string Longitude { get; set; }


        [BindProperty(SupportsGet = true)]
        public int AlbumId { get; set; }
       
        public void OnGet()
        {
            Categories = _context.Categories
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
                .ToList();

            Photo = new AddPhoto
            {
                AlbumId = AlbumId
            };
        }

        public IActionResult OnPost()
        {
            Categories = _context.Categories
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
                .ToList();


            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                             .Select(e => e.ErrorMessage)
                                             .ToList();

                foreach (var error in errors)
                {
                    Console.WriteLine("ModelState error: " + error);
                }

            }

            if (!ModelState.IsValid || UploadedFile == null || UploadedFile.Length == 0)
            {
                if (UploadedFile == null || UploadedFile.Length == 0)
                {
                    ModelState.AddModelError("UploadedFile", "The Upload field is required.");
                }
                return Page();
            }

            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(UploadedFile.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                UploadedFile.CopyTo(stream);
            }
            Photo createdPhoto = new Photo();

            createdPhoto.Name = Photo.Name;
            createdPhoto.Description = Photo.Description;
            createdPhoto.AlbumId = Photo.AlbumId;
            createdPhoto.CategoryId = Photo.CategoryId;
            createdPhoto.FilePath = "/uploads/" + uniqueFileName;
            createdPhoto.Date = DateTime.UtcNow;
            createdPhoto.UserId = _userManager.GetUserId(User);
            createdPhoto.Latitude = double.TryParse(Latitude, out var lat) ? lat : 0;
            createdPhoto.Longitude = double.TryParse(Longitude, out var lon) ? lon : 0;


            _context.Photos.Add(createdPhoto);
            _context.SaveChanges();

            return RedirectToPage("/Albums/AlbumDetails", new { id = AlbumId });

        }


    }
}

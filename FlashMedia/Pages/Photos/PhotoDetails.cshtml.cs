using FlashMedia.Data;
using FlashMedia.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMedia.Pages.Photos
{
    public class PhotoDetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PhotoDetailsModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Photo Photo { get; set; }
        public List<Comment> Comment { get; set; }

        [BindProperty]
        public AddComment AddComment { get; set; } = new AddComment();

        public IActionResult OnGet(int id)
        {
            Photo = _context.Photos.FirstOrDefault(p => p.Id == id);

            if (Photo == null)
            {
                return NotFound();
            }

            Comment = _context.Comments
                .Where(c => c.PhotoId == id)
                .Include(c => c.User)
                .OrderByDescending(c => c.DateCreated)
                .ToList();

            AddComment = new AddComment
            {
                PhotoId = id
            };

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            // Check if the comment text is empty or null
            if (string.IsNullOrWhiteSpace(AddComment.Text))
            {
                // Add a custom error message to the model state
                ModelState.AddModelError("AddComment.Text", "Comment text cannot be empty.");

                // Reload the photo and comments if the comment is empty
                Photo = _context.Photos.FirstOrDefault(p => p.Id == id);
                if (Photo == null)
                {
                    return NotFound();
                }

                Comment = _context.Comments
                    .Where(c => c.PhotoId == id)
                    .Include(c => c.User)
                    .OrderByDescending(c => c.DateCreated)
                    .ToList();

                return Page();  // Return to the same page, preserving the data
            }

            // Proceed to add the comment if it's valid
            Comment createdComment = new Comment();
            createdComment.Text = AddComment.Text;
            createdComment.DateCreated = DateTime.UtcNow;
            createdComment.UserId = _userManager.GetUserId(User);
            createdComment.PhotoId = AddComment.PhotoId;
            createdComment.State = false;  // Default state set to false (unconfirmed)
            _context.Comments.Add(createdComment);
            _context.SaveChanges();

            return RedirectToPage("/Photos/PhotoDetails", new { id = id });
        }


        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(s => s.Id == id);
            if (photo == null)
            {
                return NotFound();
            }

            // Get the current user's ID
            var currentUserId = _userManager.GetUserId(User);

            // Check if the current user is the owner of the photo or an admin
            var isOwner = photo.UserId == currentUserId;
            var isAdmin = await _userManager.IsInRoleAsync(await _userManager.FindByIdAsync(currentUserId), "Administrator");

            if (!isOwner && !isAdmin)
            {
                // If the user is not the owner and not an administrator, deny the request
                return Forbid();
            }

            _context.Photos.Remove(photo);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostDeleteCommentAsync(int commentId)
        {
            var comment = await _context.Comments
                .Include(c => c.Photo)  // Ensure the related Photo is included
                .FirstOrDefaultAsync(c => c.Id == commentId);

            if (comment == null)
            {
                return NotFound();
            }

            // Get the current user's ID
            var currentUserId = _userManager.GetUserId(User);

            // Check if the current user is the owner of the comment or an admin, or if they are the owner of the photo
            var isOwnerOfComment = comment.UserId == currentUserId;
            var isAdmin = await _userManager.IsInRoleAsync(await _userManager.FindByIdAsync(currentUserId), "Administrator");
            var isOwnerOfPhoto = comment.Photo.UserId == currentUserId;  // Accessing the loaded Photo

            if (!isOwnerOfComment && !isAdmin && !isOwnerOfPhoto)
            {
                // If the user is not the owner of the comment, not an administrator, and not the owner of the photo, deny the request
                return Forbid();
            }

            // Delete the comment
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Photos/PhotoDetails", new { id = comment.PhotoId });
        }



        public async Task<IActionResult> OnPostConfirmCommentAsync(int commentId)
        {
            var comment = await _context.Comments
                .Include(c => c.Photo)  // Ensure Photo is included to access Photo.UserId
                .FirstOrDefaultAsync(c => c.Id == commentId);

            if (comment == null)
            {
                return NotFound();
            }

            // Get the current user's ID
            var currentUserId = _userManager.GetUserId(User);

            // Check if the current user is the owner of the comment, an admin, or the owner of the photo
            var isOwnerOfComment = comment.UserId == currentUserId;
            var isAdmin = await _userManager.IsInRoleAsync(await _userManager.FindByIdAsync(currentUserId), "Administrator");
            var isOwnerOfPhoto = comment.Photo.UserId == currentUserId;  // Accessing the loaded Photo

            if (!isOwnerOfComment && !isAdmin && !isOwnerOfPhoto)
            {
                // If the user is not the owner of the comment, not an administrator, and not the owner of the photo, deny the request
                return Forbid();
            }

            // Mark the comment as confirmed
            comment.State = true;
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Photos/PhotoDetails", new { id = comment.PhotoId });
        }

    }
}
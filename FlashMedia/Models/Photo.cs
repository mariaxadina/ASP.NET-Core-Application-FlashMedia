using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FlashMedia.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        [BindNever]
        public Category Category { get; set; }
        public string UserId { get; set; }
        [BindNever]
        public IdentityUser User { get; set; }
        public int AlbumId { get; set; }
        [BindNever]
        public Album Album { get; set; }
        public string FilePath { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public List<Comment> Comments { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FlashMedia.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public bool State { get; set; } = false; 

        public DateTime DateCreated { get; set; }

        public int PhotoId { get; set; }
        public Photo Photo { get; set; }

        public string UserId { get; set; }

        [BindNever]
        public IdentityUser User { get; set; }
    }
}
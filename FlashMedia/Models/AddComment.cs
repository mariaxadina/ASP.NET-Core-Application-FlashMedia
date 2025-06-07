using Microsoft.AspNetCore.Identity;

namespace FlashMedia.Models
{
    public class AddComment
    {
        public string Text { get; set; }
        public int PhotoId { get; set; }

    }
}

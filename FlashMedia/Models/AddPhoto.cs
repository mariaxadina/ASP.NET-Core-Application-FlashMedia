using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FlashMedia.Models
{
    public class AddPhoto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }

        public int AlbumId { get; set; }
        public string Location { get; set; }

    }
}

namespace FlashMedia.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string UserId { get; set; }
        public List<Photo> Photos { get; set; }
    }
}

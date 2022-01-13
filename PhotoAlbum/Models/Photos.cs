namespace PhotoAlbum.Models
{
    public class Photos
    {
        public int AlbumId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string URL { get; set; }
        public string thumbnailURL { get; set; }
    }
}
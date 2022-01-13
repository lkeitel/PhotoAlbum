using System.Text.Json.Serialization;

namespace PhotoAlbum.Models
{
    public class Photos
    {
        [JsonPropertyName("albumId")]
        public int AlbumId { get; set; }
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("url")]
        public string URL { get; set; }
        [JsonPropertyName("thumbnailUrl")]
        public string thumbnailURL { get; set; }
    }
}
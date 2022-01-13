using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading.Tasks;
using PhotoAlbum.Models;

namespace PhotoAlbum
{
    public class PhotoServices
    {
        private HttpClient _client;
        public PhotoServices()
        {
            _client = new HttpClient();
        }

        public PhotoServices(HttpClient client)
        {
            _client = client;
        }
        public async Task<List<Photos>> GetPhotos(int? albumId)
        {
            var albumQuery =  albumId == null ? "": $"?albumId={albumId}";
            var url = "https://jsonplaceholder.typicode.com/photos" + albumQuery; 
            var result = await _client.GetAsync(url);
            var content = result.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<List<Photos>>(content);
        }

    }
}

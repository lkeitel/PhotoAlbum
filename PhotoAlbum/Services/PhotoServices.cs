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
        public async Task<List<Photos>> GetPhotos()
        {
            var result = await _client.GetAsync("https://jsonplaceholder.typicode.com/photos");
            var content = result.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<List<Photos>>(content);
        }

    }
}

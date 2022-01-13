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
        private IApiClient _client;
        public PhotoServices() : this(new ApiClient())
        {
            
        }

        public PhotoServices(IApiClient client)
        {
            _client = client;
        }
        public async Task<List<Photos>> GetPhotos(int? albumId)
        {
            var albumQuery =  albumId == null ? "": $"?albumId={albumId}";
            var url = "https://jsonplaceholder.typicode.com/photos" + albumQuery; 
            return await _client.Get<List<Photos>>(url);
        }

    }
}

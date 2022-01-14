using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoAlbum.Models;

namespace PhotoAlbum;

public interface IPhotoServices
{
    Task<List<Photos>> GetPhotos(int? albumId);
}

public class PhotoServices : IPhotoServices
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
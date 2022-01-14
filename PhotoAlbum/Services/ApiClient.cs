using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PhotoAlbum;

public interface IApiClient
{
    Task<T> Get<T>(string uri);
}

public class ApiClient : IApiClient
{
    public async Task<T> Get<T>(string uri)
    {
        var client = new HttpClient();
        var response = await client.GetAsync(uri);
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(content);
    }
}
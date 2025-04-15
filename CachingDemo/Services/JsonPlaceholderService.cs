
using CachingDemo.Models;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace CachingDemo.Services
{
    public class JsonPlaceholderService
    {
        private readonly HttpClient _httpClient;
        public JsonPlaceholderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<Post>>(content);

        }

    }
}

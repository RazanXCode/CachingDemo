using CachingDemo.Models;
using CachingDemo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Text.Json;

namespace CachingDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IDatabase _cache;
        private readonly JsonPlaceholderService _service;
        private const string CacheKey = "posts";

        public PostController(IConnectionMultiplexer redis, JsonPlaceholderService service)
        {
            _cache = redis.GetDatabase();
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            string cachedData = await _cache.StringGetAsync(CacheKey);

            if (!string.IsNullOrEmpty(cachedData))
            {
                var cachedPosts = JsonSerializer.Deserialize<IEnumerable<Post>>(cachedData);
                return Ok(new { source = "cache", data = cachedPosts });
            }

            var posts = await _service.GetPosts();
            var serialized = JsonSerializer.Serialize(posts);
            await _cache.StringSetAsync(CacheKey, serialized, TimeSpan.FromMinutes(5));

            return Ok(new { source = "api", data = posts });
        }
    }
}

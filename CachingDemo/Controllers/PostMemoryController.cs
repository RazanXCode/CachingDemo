using CachingDemo.Models;
using CachingDemo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CachingDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostMemoryController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        private readonly JsonPlaceholderService _service;
        public PostMemoryController(IMemoryCache memoryCache, JsonPlaceholderService service)
        {
            _memoryCache = memoryCache;
            _service = service;
        }

        [HttpGet("memory-posts")]
        public async Task<IActionResult> GetPostsWithMemoryCache()
        {
            const string cacheKey = "memory-posts";

            if (_memoryCache.TryGetValue(cacheKey, out IEnumerable<Post> cachedPosts))
            {
                return Ok(new { source = "memory cache", data = cachedPosts });
            }

            var posts = await _service.GetPosts();

            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(3),
               
            };

            _memoryCache.Set(cacheKey, posts, cacheEntryOptions);

            return Ok(new { source = "api", data = posts });
        }



    }
}

# .NET Caching & UI dybamic update

This project demonstrates how to fetch data from an external API and apply multiple caching strategies in a .NET Web API, along with a simple frontend using HTML and JavaScript.

## Technologies Used
- ASP.NET Core Web API (MVC)
- Redis Caching (`StackExchange.Redis`)
- In-Memory Caching (`IMemoryCache`)
- JavaScript Fetch API
- Lazy Loading Images
- Swagger for API testing

---

## Project Structure
### API Endpoints

- /posts	Get posts from JSONPlaceholder (Redis)
- /memory-posts	Get posts using in-memory caching

 ### Frontend Pages
- index.html — Dynamic post loading using Fetch API
- lazy.html — Lazy loading images from Lorem Picsum


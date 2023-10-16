

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Movie.Models;
using Movie.Options;
using System.Text.Json;

namespace Movie.Services
{
    public class MovieApiService : IMovieApiService
    {
        private readonly IMemoryCache memoryCache;
        public string BaseUrl { get; set; }
        public string ApiKey { get; set; }
        private HttpClient httpClient { get; set; }

        public MovieApiService(IHttpClientFactory httpClientFactory,IOptions<MovieApiOptions> options,IMemoryCache memoryCache)
        {

            this.memoryCache = memoryCache;
            BaseUrl = options.Value.BaseUrl;
            ApiKey = options.Value.ApiKey;

            httpClient = httpClientFactory.CreateClient();
        }

        public async Task<MovieApiResponse> SearchByTitleAsync(string title,int page=1)
        {
            MovieApiResponse result;
            if (!memoryCache.TryGetValue(title.ToLower()+page, out result))
            {
                var response = await httpClient.GetAsync($"{BaseUrl}?s={title}&apikey={ApiKey}&page={page}");
                var json = await response.Content.ReadAsStringAsync();
                result = JsonSerializer.Deserialize<MovieApiResponse>(json);
                if (result.Response == "False")
                {
                    throw new Exception(result.Error);
                }
                var cacheTime = new MemoryCacheEntryOptions();
                cacheTime.SetAbsoluteExpiration(TimeSpan.FromDays(10));
                memoryCache.Set(title+page, result, cacheTime);
            }
            return result;
        }

        public async Task<Cinema> SearchByIdAsync(string id)
        {
            Cinema result;
            if (!memoryCache.TryGetValue(id.ToLower(), out result))
            {
                var response = await httpClient.GetAsync($"{BaseUrl}?apikey={ApiKey}&i={id}");
                var json = await response.Content.ReadAsStringAsync();
                result = JsonSerializer.Deserialize<Cinema>(json);
                if (result.Response == "False")
                {
                    throw new Exception(result.Error);
                }
                var cacheTime = new MemoryCacheEntryOptions();
                cacheTime.SetAbsoluteExpiration(TimeSpan.FromDays(10));
                memoryCache.Set(id, result, cacheTime);
            }
            return result;
        }
    }
}

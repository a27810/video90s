using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Video90s.API.Dtos;

namespace Video90s.API.Services
{
    // Clases de mapeo de la API TVMaze
    internal class SearchResult
    {
        public ShowRaw show { get; set; } = null!;
    }
    internal class ShowRaw
    {
        public string name { get; set; } = null!;
        public string url { get; set; } = null!;
        public string? summary { get; set; }
        public ImageRaw? image { get; set; }
    }
    internal class ImageRaw
    {
        public string? medium { get; set; }
        public string? original { get; set; }
    }

    public class ExternalService : IExternalService
    {
        private readonly HttpClient _http;
        public ExternalService(HttpClient http) => _http = http;

        public async Task<IEnumerable<ShowDto>> SearchShowsAsync(string title)
        {
            // Llamada: GET https://api.tvmaze.com/search/shows?q={title}
            var resp = await _http.GetAsync($"search/shows?q={Uri.EscapeDataString(title)}");
            resp.EnsureSuccessStatusCode();

            var results = await resp.Content.ReadFromJsonAsync<List<SearchResult>>();
            if (results is null) return Array.Empty<ShowDto>();

            // Mapear a nuestro DTO
            return results.Select(r => new ShowDto
            {
                Name     = r.show.name,
                Url      = r.show.url,
                Summary  = r.show.summary,
                ImageUrl = r.show.image?.medium
            });
        }
    }
}

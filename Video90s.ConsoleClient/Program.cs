using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Video90s.ConsoleClient
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Genre { get; set; } = "";
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            var apiBase = Environment.GetEnvironmentVariable("API_BASE_URL") ?? "http://api:80";
            Console.WriteLine("📺 Video90s Console Client");

            Console.Write("Filtro de género (ej. Comedy) o deja vacío: ");
            var genre = Console.ReadLine()?.Trim() ?? "";

            Console.Write("¿Sólo disponibles? (s/n) o deja vacío: ");
            var availInput = Console.ReadLine()?.Trim().ToLower();
            bool? available = null;
            if (availInput == "s" || availInput == "y") available = true;
            else if (availInput == "n")          available = false;

            Console.Write("Ordenar por (price/release) o deja vacío: ");
            var sort = Console.ReadLine()?.Trim().ToLower();
            if (sort != "price" && sort != "release") sort = "";

            // Construir query string sólo con parámetros no vacíos
            var query = new List<string>();
            if (!string.IsNullOrEmpty(genre))
                query.Add($"genre={Uri.EscapeDataString(genre)}");
            if (available.HasValue)
                query.Add($"available={available.Value.ToString().ToLower()}");
            if (!string.IsNullOrEmpty(sort))
                query.Add($"sort={Uri.EscapeDataString(sort)}");

            var url = $"{apiBase}/api/movies";
            if (query.Count > 0)
                url += "?" + string.Join("&", query);

            using var client = new HttpClient();
            var res = await client.GetAsync(url);
            if (!res.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error HTTP {res.StatusCode}");
                return;
            }

            var json = await res.Content.ReadAsStringAsync();
            var opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var movies = JsonSerializer.Deserialize<List<Movie>>(json, opts) ?? new List<Movie>();

            Console.WriteLine("\nPelículas encontradas:");
            foreach (var m in movies)
            {
                Console.WriteLine($"{m.Id,3}: {m.Title} | {m.Genre} | {m.ReleaseDate:yyyy-MM-dd} | {m.Price:C} | {(m.IsAvailable ? "✔︎" : "✖︎")}");
            }
        }
    }
}

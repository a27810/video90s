namespace Video90s.API.Models
{
    public class Movie
    {
        public int Id { get; set; }                    // PK
        public string Title { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public string Format { get; set; }             // Videojuego, DVD, BluRay
    }
}

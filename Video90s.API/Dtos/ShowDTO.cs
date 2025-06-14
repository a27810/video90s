namespace Video90s.API.Dtos
{
    public class ShowDto
    {
        public string Name { get; set; } = null!;
        public string Url { get; set; } = null!;
        public string? Summary { get; set; }
        public string? ImageUrl { get; set; }
    }
}

namespace Video90s.API.Models
{
    public class Rental
    {
        public int Id { get; set; }                    // PK
        public int UserId { get; set; }
        public User User { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public DateTime RentedAt { get; set; }
        public DateTime? ReturnedAt { get; set; }
        public bool IsReserved { get; set; }
    }
}

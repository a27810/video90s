namespace Video90s.API.Models
{
    public class User
    {
        public int Id { get; set; }                    // PK
        public string UserName { get; set; }           // login
        public string Email { get; set; }
        public string PasswordHash { get; set; }       // para JWT
        public bool IsActive { get; set; }
        public DateTime RegisteredAt { get; set; }
        public string Role { get; set; }               // Admin, User, Guest
    }
}

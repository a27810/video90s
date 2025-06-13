namespace Video90s.API.Dtos
{
    public class UserRegisterDto
    {
        public string UserName   { get; set; }
        public string Email      { get; set; }
        public string Password   { get; set; }
        public string Role       { get; set; } // "User" por defecto, o "Admin"
    }
}

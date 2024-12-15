namespace StockManager.BLL.DTOs
{
    public class UserDto
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required IList<string> Roles { get; set; }
        public required string Token { get; set; }
        public required DateTime Expiration { get; set; }
    }
}

namespace ViltrapportenApi.Modal
{
    public class UserDto
    {
        public int UserId { get; set; }
        public bool IsAdmin { get; set; }
        public string DisplayName { get; set; }
        public string Token { get; set; }
        public Guid RefreshToken { get; set; }
    }
}

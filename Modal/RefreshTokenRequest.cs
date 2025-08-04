namespace ViltrapportenApi.Modal
{
    public class RefreshTokenDto
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public Guid RefreshToken { get; set; }
    }
}

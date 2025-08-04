namespace ViltrapportenApi.Modal
{
    public class TokenResponse
    {
        public string Token { get; internal set; }
        public Guid RefreshToken { get; internal set; }
        public string Error { get; internal set; }
    }
}

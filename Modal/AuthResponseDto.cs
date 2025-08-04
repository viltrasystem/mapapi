namespace ViltrapportenApi.Modal
{
    public class AuthResponseDto
    {
        public bool IsSuccess { get; set; }
        public int? UserId { get; set; }
        public bool IsAdmin { get; set; }
        public string Token { get; set; }
        public Guid RefreshToken { get; set; }
        public string DisplayName { get; set; }
        public string ErrorStatus { get; set; }
        public string Error { get; set; }

        // Factory methods for creating AuthResult objects
        public static AuthResponseDto Success(int userId, bool isAdmin, string displayName, string token, Guid refreshToken) =>
            new AuthResponseDto
            {
                IsSuccess = true,
                UserId = userId,
                IsAdmin = isAdmin,
                DisplayName = displayName,
                Token = token,
                RefreshToken = refreshToken
            };

        public static AuthResponseDto Failure(string errorStatus, string error = null) =>
            new AuthResponseDto
            {
                IsSuccess = false,
                ErrorStatus = errorStatus,
                Error = error
            };
    }

}

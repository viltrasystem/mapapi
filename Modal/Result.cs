namespace ViltrapportenApi.Modal
{
    public class Result
    {
        public bool IsSuccess { get;}
        public string Message { get; }
        public int StatusCode { get; }

        protected Result(bool isSuccess, string message = null, int statusCode = 200)
        {
            IsSuccess = isSuccess;
            Message = message;
            StatusCode = statusCode;
        }

        public static Result Success() => new Result(true);
        public static Result Failure(string message, int statusCode = 400) => new Result(false, message, statusCode);
    }

    public class Result<T> : Result
    {
        public T Value { get; }

        private Result(T value, bool isSuccess, string message = null, int statusCode = 200) : base(isSuccess, message, statusCode)
        {
            Value = value;
        }

        public static Result<T> Success(T value, string message = null, int statusCode = 200) => new Result<T>(value, true, message, statusCode);
        public static Result<T> Failure(string message, int statusCode = 400) => new Result<T>(default, false, message, statusCode);

        public void ThrowIfFailure()
        {
            if (!IsSuccess)
            {
                throw new InvalidOperationException(Message);
            }
        }

        internal static Result<TokenResponse> Failure(TokenResponse tokenResponse)
        {
            throw new NotImplementedException();
        }

        // Implicit operator to simplify usage
        public static implicit operator Result<T>(T value) => Success(value);
    }
}
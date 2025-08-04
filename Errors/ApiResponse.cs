namespace ViltrapportenApi.Errors
{
    public class ApiResponse<T>
    {
        public ApiResponse(int statusCode, T? data, string? message = null)
        {
            StatusCode = statusCode;
            Data = data;
            Message = message ?? statusCode switch
            {
                400 => "A bad request you have made",
                401 => "Authorized, you are not",
                404 => "Resource found, it was not",
                500 => "Errors are the path to the dark side. Errors led to anger. Anger leads to hate. Hate leads to career change",
                _ => "An unexpected error has occurred"
            };
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
    }
}

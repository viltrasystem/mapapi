namespace ViltrapportenApi.Errors
{
    public class ApiValidationErrorResponse : ApiResponse<string>
    {
        public ApiValidationErrorResponse() : base(400, "") /***/
        {

        }

        public IEnumerable<string> Errors { get; set; }

    }
}

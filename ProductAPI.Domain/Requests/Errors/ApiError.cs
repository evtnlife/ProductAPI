using Newtonsoft.Json;

namespace ProductAPI.Domain.Requests.Errors
{
    public class ApiError
    {
        public int StatusCode { get; private set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Message { get; private set; }

        public ApiError(int statusCode)
        {
            StatusCode = statusCode;
        }

        public ApiError(int statusCode, string message)
            : this(statusCode)
        {
            Message = message;
        }
    }
}

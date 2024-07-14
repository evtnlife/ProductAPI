namespace ProductAPI.Domain.Requests.Errors
{
    public class UnauthorizedReturn : ApiError
    {
        public UnauthorizedReturn()
            : base(401)
        {
        }

        public UnauthorizedReturn(string message)
            : base(401, message)
        {
        }
    }
}

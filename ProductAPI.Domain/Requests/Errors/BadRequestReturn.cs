namespace ProductAPI.Domain.Requests.Errors
{
    public class BadRequestReturn : ApiError
    {
        public IEnumerable<string> Conditions { get; set; }

        public BadRequestReturn()
            : base(400)
        {
        }

        public BadRequestReturn(string message)
            : base(400, message)
        {
        }

        public BadRequestReturn(string message, IEnumerable<string> conditions)
            : base(400, message)
        {
            Conditions = conditions;
        }
    }
}

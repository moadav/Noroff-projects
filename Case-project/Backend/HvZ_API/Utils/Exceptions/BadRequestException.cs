namespace HvZ_API.Utils.Exceptions
{
    public class BadRequestException: Exception
    {
        public BadRequestException(string? message) : base(message)
        {
        }
    }
}



namespace BuildingBlocks.Exceptions
{
    public class BadRequestException :Exception
    {
        public BadRequestException(string message) : base(message)
        {

        }
        public BadRequestException(string message, string details) : base(message)
        {
            this.details = details;
        }

        public string? details { get; }
    }
}

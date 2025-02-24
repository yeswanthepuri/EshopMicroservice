

using BuildingBlocks.Exceptions;

namespace Ordering.Application.Exception
{
    public class OrderNotFoundException : NotFoundException
    {
        public OrderNotFoundException(string message) : base(message)
        {
        }

        public OrderNotFoundException(string name, object key) : base(name, key)
        {
        }
    }
}

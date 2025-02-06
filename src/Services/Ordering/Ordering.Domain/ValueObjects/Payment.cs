

namespace Ordering.Domain.ValueObjects
{
    public record Payment
    {
        public string CardName { get; private set; } = default!;
        public string CardNumber { get; private set; } = default!;
        public string CardExpiration { get; private set; } = default!;
        public string CVV { get; private set; } = default!;
        public string PaymentMethod { get; private set; } = default!;
    }
}

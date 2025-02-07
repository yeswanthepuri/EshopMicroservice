

namespace Ordering.Domain.ValueObjects
{
    public record Payment
    {
        public string CardName { get; private set; } = default!;
        public string CardNumber { get; private set; } = default!;
        public string CardExpiration { get; private set; } = default!;
        public string CVV { get; private set; } = default!;
        public string PaymentMethod { get; private set; } = default!;

        protected Payment()
        { }
        private Payment(string cardName,
            string cardNumber,
            string cardExpiration,
            string cvv,
            string paymentMethod
            )
        {
            CardExpiration = cardExpiration;
            CVV = cvv;
            PaymentMethod = paymentMethod;
            CardName = cardName;
            CardNumber = cardNumber;
        }

        public static Payment Of(
            string cardName,
            string cardNumber,
            string cardExpiration,
            string cvv,
            string paymentMethod)
        {

            ArgumentException.ThrowIfNullOrWhiteSpace(cardName);
            ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber);
            ArgumentException.ThrowIfNullOrWhiteSpace(cardExpiration);
            ArgumentException.ThrowIfNullOrWhiteSpace(cvv);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length,3);

            return new Payment(
                cardName,
                cardNumber,
             cardExpiration,
             cvv,
             paymentMethod);
        }
    }


}

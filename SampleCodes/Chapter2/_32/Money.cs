using System;

namespace _32
{
    class Money
    {
        private readonly decimal amount;
        private readonly string currency;

        public Money(decimal amount, string currency)
        {
            if (currency == null) throw new ArgumentNullException(nameof(currency));

            this.amount = amount;
            this.currency = currency;
        }

        public Money Add(Money arg)
        {
            if (arg == null) throw new ArgumentNullException(nameof(arg));
            if (currency != arg.currency) throw new ArgumentException($"화폐단위가 다름(this:{currency}, arg:{arg.currency}）");

            return new Money(amount + arg.amount, currency);
        }

        public Money Multiply(Rate rate)
        {
            if (rate == null) throw new ArgumentNullException(nameof(rate));

            var result = amount * rate.Value;

            return new Money(result, currency);
        }
    }
}

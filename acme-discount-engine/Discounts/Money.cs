
namespace acme_discount_engine.Discounts
{
    public class Money
    {
        public decimal Amount { get; private set; }

        public Money(double amount)
        {
            this.Amount = (decimal)amount;
        }

        public Money(decimal amount)
        {
            Amount = amount;
        }

        public double ToDouble() => (double)Amount;

        public Money Add(Money money)
        {
            return new Money(Amount + money.Amount);
        }

        public Money Subtract(Money money)
        {
            return new Money(Amount - money.Amount);
        }

        public Money RaiseByPercent(double percent) 
        {
            decimal increase = Amount * (decimal)(percent / 100);
            return new Money(Amount + increase);
        }

        public Money DecreaseByPercent(double percent)
        {
            decimal decrease = Amount * (decimal)(percent / 100);
            return new Money(Amount - decrease);
        }

        public Money Round()
        {
            decimal scaledAmount = Amount * 100;
            decimal fractionalPart = scaledAmount - Math.Truncate(scaledAmount);

            if (fractionalPart <= 0.5m)
            {
                scaledAmount = Math.Floor(scaledAmount);
            }
            else
            {
                scaledAmount = Math.Ceiling(scaledAmount);
            }

            return new Money(scaledAmount / 100);
        }
    }
}

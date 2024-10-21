using AcmeSharedModels;

namespace acme_discount_engine.Discounts
{
    public class DiscountEngine
    {
        public bool LoyaltyCard { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;

        private List<string> NoDiscountList = new List<string> { "T-Shirt", "Keyboard", "Drill", "Chair" };

        private List<IDiscountStrategy> _discountStrategies = new List<IDiscountStrategy>
        {
            new TwoForOneDiscount(),
            new BulkDiscount()
        };

        private List<DiscountEngineItem> ProcessItems(List<Item> items)
        {
            List<DiscountEngineItem> ConvertedItems = new List<DiscountEngineItem>();
            foreach (var item in items)
            {
                if (item.IsPerishable)
                {
                    ConvertedItems.Add(new PerishableItem(item));
                }
                else
                {
                    ConvertedItems.Add(new NonPerishableItem(item));
                }
            }
            return ConvertedItems;
        }

        public double ApplyDiscounts(List<Item> items)
        {
            List<DiscountEngineItem> ProcessedList = ProcessItems(items);

            Money itemTotal = new Money(0.00);

            foreach (var item in ProcessedList)
            {
                item.ApplyDiscount(NoDiscountList, Time);
                itemTotal = itemTotal.Add(new Money(item.item.Price));
            }

            foreach (var strategy in _discountStrategies)
            {
                strategy.ApplyDiscount(ProcessedList);
            }

            itemTotal = new Money(ProcessedList.Sum(item => item.item.Price));

            if (LoyaltyCard && itemTotal.Amount >= 50.00m)
            {
                LoyaltyCardDiscount loyaltyCardDiscount = new LoyaltyCardDiscount(LoyaltyCard);
                itemTotal = loyaltyCardDiscount.ApplyDiscount(itemTotal);
            }

            return itemTotal.Round().ToDouble();
        }
    }
}


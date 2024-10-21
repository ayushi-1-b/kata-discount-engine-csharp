using AcmeSharedModels;

namespace acme_discount_engine.Discounts
{
    public class DiscountEngine
    {
        public bool LoyaltyCard { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;

        private List<string> TwoForOneList = new List<string> { "Freddo" };

        private List<string> NoDiscountList = new List<string> { "T-Shirt", "Keyboard", "Drill", "Chair" };

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

        private void ApplyTwoForOneDiscount(List<DiscountEngineItem> items)
        {
            items.Sort((x, y) => x.item.Name.CompareTo(y.item.Name));
            string currentItem = string.Empty;
            int itemCount = 0;

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].item.Name != currentItem)
                {
                    currentItem = items[i].item.Name;
                    itemCount = 1;
                }
                else
                {
                    itemCount++;
                    if (itemCount == 3 && TwoForOneList.Contains(items[i].item .Name))
                    {
                        items[i].item.Price = 0.00;
                        itemCount = 0;
                    }
                }
            }
        }

        private void ApplyBulkDiscounts(List<DiscountEngineItem> items)
        {
            string currentItem = string.Empty;
            int itemCount = 0;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].item.Name != currentItem)
                {
                    currentItem = items[i].item.Name;
                    itemCount = 1;
                }
                else
                {
                    itemCount++;
                    if (itemCount == 10 && !TwoForOneList.Contains(items[i].item.Name) && items[i].item.Price >= 5.00)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            Money price = new Money(items[i - j].item.Price);
                            price = price.DecreaseByPercent(2.0);
                            items[i - j].item.Price = price.ToDouble();
                        }
                        itemCount = 0;
                    }
                }
            }
        }

        public double ApplyLoyaltyCardDiscount(double total)
        {
            Money totalMoney = new Money(total);
            totalMoney = totalMoney.DecreaseByPercent(2);
            return totalMoney.ToDouble();
        }

        public double ApplyDiscounts(List<Item> items)
        {

            List<DiscountEngineItem> ProcessedList = ProcessItems(items);

            ApplyTwoForOneDiscount(ProcessedList);

            Money itemTotal = new Money(0.00);

            foreach (var item in ProcessedList)
            {
                itemTotal = itemTotal.Add(new Money(item.item.Price));
                // only applied for perishable and non perishable for now.
                item.ApplyDiscount(NoDiscountList, Time);
            }

            ApplyBulkDiscounts(ProcessedList);

            Money finalTotal = new Money(ProcessedList.Sum(item => item.item.Price));

            if (LoyaltyCard && itemTotal.Amount >= 50.00m)
            {
                finalTotal = new Money(ApplyLoyaltyCardDiscount(finalTotal.ToDouble()));
            }

            return finalTotal.Round().ToDouble();
        }
    }
}


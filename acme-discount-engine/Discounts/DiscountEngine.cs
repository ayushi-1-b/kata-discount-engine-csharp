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
                            items[i - j].item.Price -= items[i - j].item.Price * 0.02;
                        }
                        itemCount = 0;
                    }
                }
            }
        }

        public double ApplyLoyaltyCardDiscount(double total)
        {
            total -= total * 0.02;
            return total;
        }

        public double ApplyDiscounts(List<Item> items)
        {
            List<DiscountEngineItem> ProcessedList = ProcessItems(items);

            ApplyTwoForOneDiscount(ProcessedList);

            double itemTotal = 0.00;

            foreach (var item in ProcessedList)
            {
                itemTotal += item.item.Price;

                // only applied for perishable and non perishable for now.
                item.ApplyDiscount(NoDiscountList);

            }

            ApplyBulkDiscounts(ProcessedList);

            double finalTotal = ProcessedList.Sum(item => item.item.Price);

            if (LoyaltyCard && itemTotal >= 50.00)
            {
                finalTotal = ApplyLoyaltyCardDiscount(finalTotal);
            }

            return Math.Round(finalTotal, 2);
        }
    }
}


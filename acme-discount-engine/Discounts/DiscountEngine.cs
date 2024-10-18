using AcmeSharedModels;

namespace acme_discount_engine.Discounts
{
    public class DiscountEngine
    { 
        public bool LoyaltyCard { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;

        private List<string> TwoForOneList = new List<string> { "Freddo" };
        private List<string> NoDiscount = new List<string> { "T-Shirt", "Keyboard", "Drill", "Chair" };

        public void ApplyTwoForOneDiscount(List<Item> items)
        {
            items.Sort((x, y) => x.Name.CompareTo(y.Name));
            string currentItem = string.Empty;
            int itemCount = 0;

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Name != currentItem)
                {
                    currentItem = items[i].Name;
                    itemCount = 1;
                }
                else
                {
                    itemCount++;
                    if (itemCount == 3 && TwoForOneList.Contains(items[i].Name))
                    {
                        items[i].Price = 0.00;
                        itemCount = 0;
                    }
                }
            }
        }

        public void ApplyPerishableItemDiscount(Item item, int daysUntilDate)
        {
            if (daysUntilDate == 0)
            {
                if (Time.Hour >= 0 && Time.Hour < 12)
                {
                    item.Price -= item.Price * 0.05;
                }
                else if (Time.Hour >= 12 && Time.Hour < 16)
                {
                    item.Price -= item.Price * 0.10;
                }
                else if (Time.Hour >= 16 && Time.Hour < 18)
                {
                    item.Price -= item.Price * 0.15;
                }
                else if (Time.Hour >= 18)
                {
                    item.Price -= item.Price * (!item.Name.Contains("(Meat)") ? 0.25 : 0.15);
                }
            }
        }

        public void ApplyNonPerishableItemDiscount(Item item, int daysUntilDate)
        {
            if (!NoDiscount.Contains(item.Name))
            {
                if (daysUntilDate >= 6 && daysUntilDate <= 10)
                {
                    item.Price -= item.Price * 0.05;
                }
                else if (daysUntilDate >= 0 && daysUntilDate <= 5)
                {
                    item.Price -= item.Price * 0.10;
                }
                else if (daysUntilDate < 0)
                {
                    item.Price -= item.Price * 0.20;
                }
            }

        }

        public void ApplyBulkDiscounts(List<Item> items)
        {
            string currentItem = string.Empty;
            int itemCount = 0;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Name != currentItem)
                {
                    currentItem = items[i].Name;
                    itemCount = 1;
                }
                else
                {
                    itemCount++;
                    if (itemCount == 10 && !TwoForOneList.Contains(items[i].Name) && items[i].Price >= 5.00)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            items[i - j].Price -= items[i - j].Price * 0.02;
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
            ApplyTwoForOneDiscount(items);
            double itemTotal = 0.00;
            foreach (var item in items)
            {
                itemTotal += item.Price;
                int daysUntilDate = (item.Date - DateTime.Today).Days;
                if(DateTime.Today > item.Date) { daysUntilDate = -1; }

                if (!item.IsPerishable)
                {
                    ApplyNonPerishableItemDiscount(item,daysUntilDate);
                }
                else
                {
                    ApplyPerishableItemDiscount(item, daysUntilDate);
                }
            }

            ApplyBulkDiscounts(items);

            double finalTotal = items.Sum(item => item.Price);

            if (LoyaltyCard && itemTotal >= 50.00)
            {
                finalTotal = ApplyLoyaltyCardDiscount(finalTotal);
            }

            return Math.Round(finalTotal, 2);
        }
    }
}


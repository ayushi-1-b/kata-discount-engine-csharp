namespace acme_discount_engine.Discounts
{
    public class BulkDiscount : IDiscountStrategy
    {
        List<string> TwoForOneNotElligibleForBulk {  get; set; }

        public BulkDiscount() 
        {
            this.TwoForOneNotElligibleForBulk = new List<string> { "Freddo" };
        }
        public BulkDiscount(List<string> InelligibleItems) 
        {
            this.TwoForOneNotElligibleForBulk = InelligibleItems;
        }
        public void ApplyDiscount(List<DiscountEngineItem> items)
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
                    if (itemCount == 10 && !TwoForOneNotElligibleForBulk.Contains(items[i].item.Name) && items[i].item.Price >= 5.00)
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
    }
}

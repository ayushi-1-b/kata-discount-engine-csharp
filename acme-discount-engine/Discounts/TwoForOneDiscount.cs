
namespace acme_discount_engine.Discounts
{
    public class TwoForOneDiscount : IDiscountStrategy
    {
        List<string> TwoForOneElligibleItems;

        public TwoForOneDiscount()
        {
            this.TwoForOneElligibleItems = new List<string> { "Freddo" };
        }
        public TwoForOneDiscount(List<string> twoForOneElligibleItems) 
        {
            this.TwoForOneElligibleItems = twoForOneElligibleItems;
        }

        public void ApplyDiscount(List<DiscountEngineItem> items) 
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
                    if (itemCount == 3 && TwoForOneElligibleItems.Contains(items[i].item.Name))
                    {
                        items[i].item.Price = 0.00;
                        itemCount = 0;
                    }
                }
            }
        }
    }
}

using AcmeSharedModels;

namespace acme_discount_engine.Discounts
{
    internal class PerishableItem : DiscountEngineItem
    {
        public Item item { get; set; }
        public int Quantity { get ; set; } = 0;
        public void ApplyDiscount(List<string> NoDiscount,DateTime time )
        {
            int daysUntilDate = (this.item.Date - DateTime.Today).Days;
            if (DateTime.Today > this.item.Date) { daysUntilDate = -1; }

            if (daysUntilDate == 0)
            {
                if (time.Hour >= 0 && time.Hour < 12)
                {
                    this.item.Price -= this.item.Price * 0.05;
                }
                else if (time.Hour >= 12 && time.Hour < 16)
                {
                    this.item.Price -= this.item.Price * 0.10;
                }
                else if (time.Hour >= 16 && time.Hour < 18)
                {
                    this.item.Price -= this.item.Price * 0.15;
                }
                else if (time.Hour >= 18)
                {
                    this.item.Price -= this.item.Price * (!this.item.Name.Contains("(Meat)") ? 0.25 : 0.15);
                }
            }
        }

        public PerishableItem(Item item) 
        {
            this.item = item;
        }
    }
}

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

            Money amount = new Money(this.item.Price);
            
            if (daysUntilDate == 0)
            {
                if (time.Hour >= 0 && time.Hour < 12)
                {
                    this.item.Price = amount.DecreaseByPercent(5.0).ToDouble();
                }
                else if (time.Hour >= 12 && time.Hour < 16)
                {
                    this.item.Price =  amount.DecreaseByPercent(10.0).ToDouble() ;
                }
                else if (time.Hour >= 16 && time.Hour < 18)
                {
                    this.item.Price = amount.DecreaseByPercent(15.0).ToDouble();
                }
                else if (time.Hour >= 18)
                {
                    this.item.Price = amount.DecreaseByPercent(!this.item.Name.Contains("(Meat)") ? 25.0 : 15.0).ToDouble();
                }
            }
        }

        public PerishableItem(Item item) 
        {
            this.item = item;
        }
    }
}

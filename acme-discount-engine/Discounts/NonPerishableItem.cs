using AcmeSharedModels;


namespace acme_discount_engine.Discounts
{
    internal class NonPerishableItem : DiscountEngineItem
    {
        public Item item { get; set; }
        public int Quantity { get; set; } = 0;
       
        public void ApplyDiscount(List<string> NoDiscount, DateTime time)
        {
            int daysUntilDate = (this.item.Date - DateTime.Today).Days;
            if (DateTime.Today >  this.item.Date) { daysUntilDate = -1; }

            Money amount = new Money(this.item.Price);

            if (!NoDiscount.Contains(this.item.Name))
            {
                if (daysUntilDate >= 6 && daysUntilDate <= 10)
                {
                    this.item.Price = amount.DecreaseByPercent(5.0).ToDouble();
                }
                else if (daysUntilDate >= 0 && daysUntilDate <= 5)
                {
                     this.item.Price = amount.DecreaseByPercent(10.0).ToDouble();
                }
                else if (daysUntilDate < 0)
                {
                    this.item.Price = amount.DecreaseByPercent(20.0).ToDouble();
                }
            }
        }

        public NonPerishableItem(Item item) 
        {
            this.item = item;
        }
    }
}

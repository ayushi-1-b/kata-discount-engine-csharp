using AcmeSharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            if (!NoDiscount.Contains(this.item.Name))
            {
                if (daysUntilDate >= 6 && daysUntilDate <= 10)
                {
                    this.item.Price -= this.item.Price * 0.05;
                }
                else if (daysUntilDate >= 0 && daysUntilDate <= 5)
                {
                     this.item.Price -= this.item.Price * 0.10;
                }
                else if (daysUntilDate < 0)
                {
                    this.item.Price -= this.item.Price * 0.20;
                }
            }
        }

        public NonPerishableItem(Item item) 
        {
            this.item = item;
        }
    }
}

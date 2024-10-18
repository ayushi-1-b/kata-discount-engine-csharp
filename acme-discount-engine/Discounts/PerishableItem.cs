using AcmeSharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace acme_discount_engine.Discounts
{
    internal class PerishableItem : DiscountEngineItem
    {
        public Item item { get; set; }
        public int Quantity { get ; set; } = 0;
        public DateTime Time { get; set; } = DateTime.Now;
        public void ApplyDiscount(List<string> NoDiscount)
        {
            int daysUntilDate = (item.Date - DateTime.Today).Days;
            if (DateTime.Today > item.Date) { daysUntilDate = -1; }

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

        public PerishableItem(Item item) 
        {
            this.item = item;
        }
    }
}

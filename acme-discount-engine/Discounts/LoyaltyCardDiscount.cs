using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace acme_discount_engine.Discounts
{
    public class LoyaltyCardDiscount
    {
        public bool LoyaltyCard { get; set; }
        public LoyaltyCardDiscount() { }
        public LoyaltyCardDiscount(bool LoyaltyCard) 
        {
            this.LoyaltyCard = LoyaltyCard;
        }

        public Money ApplyDiscount(Money total)
        {
            total = total.DecreaseByPercent(2);
            return total;
        }

    }
}

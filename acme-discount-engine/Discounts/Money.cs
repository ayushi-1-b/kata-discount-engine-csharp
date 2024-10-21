using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace acme_discount_engine.Discounts
{
    public class Money
    {
        decimal _amount;

        public Money(double amount)
        {
            this._amount = (decimal)amount;
        }

        public decimal Add(Money money) { return _amount + money._amount; }

        public decimal RaiseByPercent(double percent) { return _amount + (decimal)(percent / 100); }
    }
}

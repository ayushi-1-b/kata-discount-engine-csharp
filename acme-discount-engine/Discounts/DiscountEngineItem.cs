using AcmeSharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace acme_discount_engine.Discounts
{
    internal interface DiscountEngineItem
    {
        Item item { get; set; }

        int Quantity { get; set; }

        void ApplyDiscount(List<string> NoDiscount);
    }
}

using acme_discount_engine.Discounts;

namespace DiscountTests
{
    [TestClass]
    public class MoneyTests
    {
        [TestMethod]
        public void TestAddMoney()
        {
            Money amount1 = new Money(2.46);
            Money amount2 = new Money(8.0);

            decimal  result = amount1.Add(amount2);

            //TODO: Assert
        }

        [TestMethod]
        public void TestRaiseByPercent() {
            Money amount = new Money(45.66);
            double percent = 25;

            decimal result = amount.RaiseByPercent(percent);

            //TODO: Assert 
        }
    }
}

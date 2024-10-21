using acme_discount_engine.Discounts;

namespace DiscountTests
{
    [TestClass]
    public class MoneyTests
    {
        [TestMethod]
        public void TestAddMoney()
        {
            // Arrange
            Money amount1 = new Money(2.46);
            Money amount2 = new Money(8.0);

            // Act
            Money result = amount1.Add(amount2);

            // Assert
            Assert.AreEqual(10.46m, result.Amount, "Addition of two Money amounts did not return the expected result.");
        }

        [TestMethod]
        public void TestRaiseByPercent()
        {
            // Arrange
            Money amount = new Money(45.66);
            double percent = 25;

            // Act
            Money result = amount.RaiseByPercent(percent);

            // Assert
            Assert.AreEqual(57.075m, result.Amount, "Raising the amount by percent did not return the expected result.");
        }

        [TestMethod]
        public void TestDecreaseByPercent()
        {
            // Arrange
            Money amount = new Money(45.66);
            double percent = 25;

            // Act
            Money result = amount.DecreaseByPercent(percent);

            // Assert
            Assert.AreEqual(34.245m, result.Amount, "Raising the amount by percent did not return the expected result.");
        }

        [TestMethod]
        public void TestSubtractMoney()
        {

            // Arrange
            Money amount1 = new Money(8.0);
            Money amount2 = new Money(2.46);
            //Act
            Money result = amount1.Subtract(amount2); 
            //Assert
            Assert.AreEqual(5.54m, result.Amount, "Subtraction of two Money amounts did not return the expected result.");
        }

        [TestMethod]
        public void TestRoundDownToPenny()
        {
            Money amount = new Money(67.50500);
            Money roundedResult = amount.Round();
            Assert.AreEqual(67.50m, roundedResult.Amount, "Rounding down did not return the expected result.");
        }

        [TestMethod]
        public void TestRoundUpToPenny()
        {
            Money amount = new Money(67.51500);
            Money roundedResult = amount.Round();
            Assert.AreEqual(67.51m, roundedResult.Amount, "Rounding up did not return the expected result.");
        }

        [TestMethod]
        public void TestRoundExactPenny()
        {
            Money amount = new Money(67.52500);
            Money roundedResult = amount.Round();
            Assert.AreEqual(67.52m, roundedResult.Amount, "Rounding to the exact penny did not return the expected result.");
        }

        [TestMethod]
        public void TestRoundUpJustAboveHalf()
        {
            Money amount = new Money(67.50501);
            Money roundedResult = amount.Round();
            Assert.AreEqual(67.51m, roundedResult.Amount, "Rounding just above half did not return the expected result.");
        }

        [TestMethod]
        public void TestRoundUpNearThreshold()
        {
            Money amount = new Money(67.50750);
            Money roundedResult = amount.Round();
            Assert.AreEqual(67.51m, roundedResult.Amount, "Rounding just above .5 did not return the expected result.");
        }
    }
}

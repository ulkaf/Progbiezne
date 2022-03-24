using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testy
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAdd()
        {
            Calculator.Calculator x = new Calculator.Calculator();

            double y = x.Addition(2, 2);

            Assert.AreEqual(y, 4);

        }

        [TestMethod]
        public void TestSub()
        {
            Calculator.Calculator x = new Calculator.Calculator();
            double y = x.Subtraction(2, 2);
            Assert.AreEqual(y, 0);

        }


        [TestMethod]
        public void TestMultiply()
        {
            Calculator.Calculator x = new Calculator.Calculator();

            double y = x.Multiplication(2, 2);
            Assert.AreEqual(y, 4);

        }

        [TestMethod]
        public void TestDiv()
        {
            Calculator.Calculator x = new Calculator.Calculator();
            double y = x.Division(2, 2);
            Assert.AreEqual(y, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void DivideException()
        {
            Calculator.Calculator x = new Calculator.Calculator();
            x.Division(2, 0);
        }
    }
}
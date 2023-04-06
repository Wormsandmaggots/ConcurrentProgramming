using App;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Calculator calculator = new Calculator();
            Assert.AreEqual(calculator.Add(1, 2), 3);
        }
    }
}
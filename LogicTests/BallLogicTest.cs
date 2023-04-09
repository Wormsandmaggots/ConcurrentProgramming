
using Logic;

namespace LogicTests
{
    [TestClass]
    public class BallLogicTest
    {
        private int _x, _y, _radius;

        [TestInitialize]
        public void Initialize()
        {
            _radius = 1;
        }

        [TestMethod]
        public void CreateBallTest()
        {
            BallLogic b = new BallLogic(_x, _y, _radius);

            Assert.IsNotNull(b);
            Assert.AreEqual(b.X, _x);
            Assert.AreEqual(b.Y, _y);
            Assert.AreEqual(b.Radius, _radius);
        }

        public void ChangeBallLogicParametres()
        {
            BallLogic b = new BallLogic(_x, _y, _radius);

            b.X = 1;
            b.Y = 1;

            Assert.AreEqual(b.X, _x);
            Assert.AreEqual(b.Y, _y);
        }
    }
}

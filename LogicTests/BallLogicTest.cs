
using Logic;

namespace LogicTests
{
    [TestClass]
    public class BallLogicTest
    {
        private int _x, _y, _radius;
        private int _xBorder, _yBorder;
        private int _moveDistance;

        [TestInitialize]
        public void Initialize()
        {
            _radius = 1;
            _moveDistance = 1;

            _x = 5;
            _y = 5;

            _xBorder = 640;
            _yBorder = 640;
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

        [TestMethod]
        public void ChangeBallLogicParametres()
        {
            BallLogic b = new BallLogic(_x, _y, _radius);

            b.X = 1;
            b.Y = 1;

            Assert.AreEqual(b.X, 1);
            Assert.AreEqual(b.Y, 1);
        }

        [TestMethod]
        public void MoveBallTest()
        {
            BallLogic b = new BallLogic(_x, _y, _radius);

            for(int i = 0; i < 10; i++)
            {
                b.MoveBallRandomly(_xBorder, _yBorder, _moveDistance);

                Assert.IsTrue(b.X > _radius && b.X < _xBorder - _radius);
                Assert.IsTrue(b.Y > _radius && b.Y < _yBorder - _radius);
            }
        }
    }
}

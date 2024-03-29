﻿
namespace LogicTests
{
    [TestClass]
    public class BallLogicTest
    {
        private int _x, _y, _radius;
        private int _xBorder, _yBorder;
        private AbstractLogicApi _api;

        [TestInitialize]
        public void Initialize()
        {
            _radius = 1;

            _x = 5;
            _y = 5;

            _xBorder = 640;
            _yBorder = 640;

            _api = CreateLogicTestApi.GetLogicTestApi();
        }

        [TestMethod]
        public void CreateBallTest()
        {
            IBallLogic b = _api.CreateBall(_x, _y, _radius, _xBorder, _yBorder);

            Assert.IsNotNull(b);
            Assert.AreEqual(b.Radius, _radius);
        }

        [TestMethod]
        public void CanColideTest()
        {
            IBallLogic b = _api.CreateBall(_x, _y, _radius, _xBorder, _yBorder);

            b.SetCanCollide(true);

            Assert.AreEqual(true, b.CanCollide());


        }

    }
}


using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using System.Numerics;

namespace LogicTests
{
    [TestClass]
    public class LogicApiTests
    {
        private AbstractLogicApi _logicApi;
        private int _x1, _y1, _x2, _y2, _radius, _weight;
        private int _width, _height, _amount;
        private LogicTestApi _logicTestApi;

        [TestInitialize]
        public void Initialize()
        {
            _logicApi = CreateLogicTestApi.GetLogicTestApi();
            _radius = 1;
            _width = 640;
            _height = 640;
            _amount = 5;
            _weight = 1;
            _logicTestApi = new LogicTestApi();
         
        }

        [TestMethod]
        public void CreateBallLogicTest()
        {
            IBallLogic b = _logicApi.CreateBall(_x1, _y1, _radius, _width, _height);

            Assert.IsNotNull(b);
            Assert.AreEqual(b.Radius, _radius);
        }

        [TestMethod]
        public void CreateSceneLogicTest()
        {
            _logicApi.CreateScene(_width, _height, _amount, _radius);

            Assert.AreEqual(_logicApi.GetBalls().Count, 5);
            Assert.IsTrue(_logicApi.IsEnabled());

            _logicApi.Disable();
            Assert.IsTrue(!_logicApi.IsEnabled());

            _logicApi.Enable();
            Assert.IsTrue(_logicApi.IsEnabled());
        }

        [TestMethod]
        public void BorderCollisionTest()
        {
            _logicTestApi.CreateScene(_width, _height, 0, _radius);
            _x1 = _width + 1;
            _y1 = _height + 1;


            IBallLogic ball = _logicTestApi.CreateBall(_x1, _y1, _radius, _width, _height);

            Vector2 velVec = ball.Velocity;

            if(velVec.X < 0)
            {
                velVec.X = -velVec.X;
            }
            if (velVec.Y< 0)
            {
                velVec.Y = -velVec.Y;
            }

            ball.Velocity = velVec;

            _logicTestApi.CheckCollision(ball);

            Vector2 PostvelVec = ball.Velocity;

            Assert.AreEqual(ball.Velocity.X, -velVec.X);
            Assert.AreEqual(ball.Velocity.Y, -velVec.Y);


            _x1 = 0;
            _y1 = 0;

            IBallLogic ball2 = _logicTestApi.CreateBall(_x1, _y1, _radius, _width, _height);


            velVec = ball2.Velocity;

            if (velVec.X > 0)
            {
                velVec.X = -velVec.X;
            }
            if (velVec.Y > 0)
            {
                velVec.Y = -velVec.Y;
            }

            ball2.Velocity = velVec;

            _logicTestApi.CheckCollision(ball2);

            PostvelVec = ball2.Velocity;

            Assert.AreEqual(PostvelVec.X, -velVec.X);
            Assert.AreEqual(PostvelVec.Y, -velVec.Y);

        }

        [TestMethod]
        public void BallCollisionTest()
        {
            _logicTestApi.CreateScene(_width, _height, 0, _radius);


            _x2 = 300;
            _y2 = 300;

            _x1 = _x2;
            _y1 = _y2;
            IBallLogic ball = _logicTestApi.CreateBall(_x1, _y1, _radius, _width, _height);
            IBallLogic ball2 = _logicTestApi.CreateBall(_x2, _y2, _radius, _width, _height);


            List<Vector2> afterCollision = _logicTestApi.newVelocity(ball.Velocity, ball.Position, ball2.Velocity, ball2.Position);

            Vector2 velVec = ball.Velocity;
            Vector2 velVec2 = ball2.Velocity;

            Assert.AreNotEqual(afterCollision[0], velVec);
            Assert.AreNotEqual(afterCollision[1], velVec2);

            Assert.AreEqual(true, ball.CanCollide());
            Assert.AreEqual(true, ball2.CanCollide());


        }
    }
}
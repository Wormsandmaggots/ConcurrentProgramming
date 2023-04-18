
namespace LogicTests
{
    [TestClass]
    public class LogicApiTests
    {
        private AbstractLogicApi _logicApi;
        private int _x, _y, _radius;
        private int _width, _height, _amount;

        [TestInitialize]
        public void Initialize()
        {
            _logicApi = AbstractLogicApi.CreateApi();
            _radius = 1;
            _width = 640;
            _height = 640;
            _amount = 5;
        }

        [TestMethod]
        public void CreateBallLogicTest()
        {
            IBallLogic b = _logicApi.CreateBall(_x, _y, _radius, _width, _height);

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
    }
}
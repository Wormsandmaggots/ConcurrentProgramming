using Data;

namespace DataTests
{
    [TestClass]
    public class DataApiTest
    {
        private AbstractDataApi _dataApi;
        private int _x, _y, _radius;

        [TestInitialize]
        public void Initialize()
        {
            _dataApi = AbstractDataApi.CreateDataApi();
            _radius = 1;
        }

        [TestMethod]
        public void CreateBallTest()
        {
            Ball b = _dataApi.CreateBall(_x, _y, _radius);

            Assert.IsNotNull(b, null);
            Assert.AreEqual(b.X, _x);
            Assert.AreEqual(b.Y, _y);
            Assert.AreEqual(b.Radius, _radius);
        }

        [TestMethod]
        public void ChangeBallParametres()
        {
            Ball b = _dataApi.CreateBall(_x, _y, _radius);

            b.X = 1;
            b.Y = 1;

            Assert.AreEqual(b.X, 1);
            Assert.AreEqual(b.Y, 1);
        }
    }
}
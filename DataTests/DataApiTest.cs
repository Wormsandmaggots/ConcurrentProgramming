using Data;

namespace DataTests
{
    [TestClass]
    public class DataApiTest
    {
        private AbstractDataApi _dataApi;
        private int _x, _y, _radius;
        private int _width, _height;

        [TestInitialize]
        public void Initialize()
        {
            _dataApi = CreateDataTestApi.GetDataTestApi();
            _radius = 1;

            _width = 520;
            _height = 400;
        }

        [TestMethod]
        public void CreateBallTest()
        {
            IBall b = _dataApi.CreateBall(_x, _y, _radius, _width, _height);

            Assert.IsNotNull(b, null);
            Assert.AreEqual(b.Radius, _radius);
        }
    }
}
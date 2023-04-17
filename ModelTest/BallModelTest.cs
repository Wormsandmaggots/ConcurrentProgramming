

using Logic;
using Model;

namespace BallModelTest
{
    [TestClass]
    public class ModelApiTest
    {
        AbstractModelApi api;
        AbstractLogicApi logicApi;
        IBallLogic b;
        IBallModel ballModel;
        public int radius;
        public int x;
        public int y;



        [TestInitialize]
        public void Initialize()
        {   
            logicApi = AbstractLogicApi.CreateApi();
            api = AbstractModelApi.CreateApi();
            x = 5;
            y = 5;
            radius = 1;

            b = logicApi.CreateBall(x, y, radius);
            ballModel = api.CreateBall(b);
        }

        [TestMethod]
        public void CreateBallModelTest()
        {
            

            Assert.AreEqual(ballModel.RadiusHandler, b.Radius);
            Assert.AreEqual(ballModel.XHandler, b.X);
            Assert.AreEqual(ballModel.YHandler, b.Y);
            
        }

        [TestMethod]
        public void  ChangeValueModelTest()
        {

            ballModel.XHandler = 1;
            ballModel.YHandler = 2;

            Assert.AreEqual(ballModel.XHandler, 1);
            Assert.AreEqual(ballModel.YHandler, 2);
        }
    }
}
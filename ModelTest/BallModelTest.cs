

using Logic;
using Model;

namespace BallModelTest
{
    [TestClass]
    public class ModelApiTest
    {
        AbstractModelApi api;
        AbstractLogicApi logicApi;
        IBallLogic ballLogic;
        public int radius;
        public int x;
        public int y;



        [TestInitialize]
        public void Initialize()
        {
            logicApi = AbstractLogicApi.CreateApi();
            x = 5;
            y = 5;
            radius = 1;

            ballLogic =  logicApi.CreateBall(x, y, radius);

        }

        [TestMethod]
        public void CreateBallModelTest()
        {
            IBallModel ballModel = api.CreateBall(ballLogic);

           // Assert.AreEqual(ballModel.Radius, ballLogic.Radius);
           // Assert.AreEqual(ballModel.X, ballLogic.X);
           // Assert.AreEqual(ballModel.Y, ballLogic.Y);
            
        }

        [TestMethod]
        public void  ChangeValueModelTest()
        {
            IBallModel ballModel = api.CreateBall(ballLogic);

            ballModel.X = 1;
            ballModel.Y = 2;

            Assert.AreEqual(ballModel.X, 1);
            Assert.AreEqual(ballModel.Y, 2);
        }
    }
}
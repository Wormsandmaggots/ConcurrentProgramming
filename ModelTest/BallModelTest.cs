

using Logic;
using Model;

namespace BallModelTest
{
    [TestClass]
    public class ModelApiTest
    {

        AbstractLogicApi logicApi;
        BallLogic ballLogic;
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
            BallModel ballModel = new BallModel(ballLogic);

            Assert.AreEqual(ballModel.radius, ballLogic.Radius);
            Assert.AreEqual(ballModel.x, ballLogic.X);
            Assert.AreEqual(ballModel.y, ballLogic.Y);
            
        }

        [TestMethod]
        public void  ChangeValueModelTest()
        {
            BallModel ballModel = new BallModel(ballLogic);

            ballModel.x = 1;
            ballModel.y = 2;

            Assert.AreEqual(ballModel.x, 1);
            Assert.AreEqual(ballModel.y, 2);
        }
    }
}
using Logic;
using Model;
using System.Collections.ObjectModel;

namespace ModelTest
{
    [TestClass]
    public class ModelApiTest
    {
        
        public int ballsCount;
        public int radius;
        AbstractLogicApi logicApi;
        AbstractModelApi api;


        [TestInitialize] 
        public void Initialize() 
        {
            logicApi = AbstractLogicApi.CreateApi();
            api = AbstractModelApi.CreateApi(logicApi);
            radius = 1;
            ballsCount = 10;

            api.MakeScene(ballsCount, radius);

        }

        [TestMethod]
        public void MakeSceneTest()
        {
            ObservableCollection<BallModel> balls = api.GetAllBalls();
            
            Assert.AreEqual(ballsCount, balls.Count);
            Assert.IsNotNull(balls);

            foreach (BallModel ball in balls)
            {
                Assert.IsNotNull(ball);
                Assert.AreEqual(radius, ball.radius);
            }
        }

        [TestMethod]
        public void DisableEnableTest() {

            Assert.IsTrue(api.IsEnabled());

            api.Enable();
            Assert.IsTrue(api.IsEnabled());

            api.Disable();
            Assert.IsFalse(api.IsEnabled());
        }
    }
}
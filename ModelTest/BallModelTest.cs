
using Logic;
using Model;
using ModelTest;

namespace BallModelTest
{
    [TestClass]
    public class ModelApiTest
    {
        AbstractModelApi api;
        IBallLogic b;
        IBallModel ballModel;
        public int radius;
        public int x;
        public int y;
        private int width, height;



        [TestInitialize]
        public void Initialize()
        {
            //logicApi = AbstractLogicApi.CreateApi();
            api = CreateModelApiTest.GetModelTestApi();
           
            x = 5;
            y = 5;
            radius = 1;

            width = 640;
            height = 640;

           // b = logicApi.CreateBall(x, y, radius, width, height);
           // ballModel = api.CreateBall(b);
        }

    }
}
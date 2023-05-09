using DataTests;
using Data;


namespace LogicTests
{
    public abstract class CreateLogicTestApi
    {
        public static AbstractLogicApi GetLogicTestApi()
        {
            return new LogicTestApi();
        }
    }

    internal class LogicTestApi : AbstractLogicApi
    {
        private AbstractDataApi _dataApi;

        private IScene _scene;

        public LogicTestApi()
        {
            _dataApi = CreateDataTestApi.GetDataTestApi();
        }

        public override void CreateScene(int width, int height, int ballsAmount, int radius)
        {
            if (_scene != null)
            {
                foreach (IBallLogic ballLogic in GetBalls())
                {
                    ballLogic.Dispose();
                }
            }

            _scene = new TestScene(width, height);
            _scene.GenerateBallsList(ballsAmount, radius);
        }
        public override IBallLogic CreateBall(int x, int y, int radius, int width, int height)
        {
            return new TestBallLogic(_dataApi.CreateBall(x, y, radius, width, height));
        }

        public override List<IBallLogic> GetBalls()
        {
            return _scene.Balls;
        }

        public override void Enable()
        {
            _scene.Enabled = true;
        }

        public override void Disable()
        {
            _scene.Enabled = false;
        }

        public override bool IsEnabled()
        {
            return _scene.Enabled;
        }
    }
}

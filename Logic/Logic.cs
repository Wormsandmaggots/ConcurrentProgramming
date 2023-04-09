using Data;

namespace Logic
{
    public abstract class AbstractLogicApi
    {
        public abstract void CreateScene(int width, int height, int ballsAmount, int radius);

        public abstract BallLogic CreateBall(int x, int y, int radius);
        public abstract List<BallLogic> GetBalls();
        public abstract void Enable();
        public abstract void Disable();
        public abstract bool IsEnabled();
        public static AbstractLogicApi CreateApi()
        {
            return new LogicApi();
        }

        internal sealed class LogicApi : AbstractLogicApi
        {
            private AbstractDataApi _dataApi;

            private Scene _scene;

            public LogicApi()
            {
                _dataApi = AbstractDataApi.CreateDataApi();
            }

            public override void CreateScene(int width, int height, int ballsAmount, int radius)
            {
                _scene = new Scene(width, height);
                _scene.GenerateBallsList(ballsAmount, radius);

                foreach (BallLogic ball in _scene.Balls)
                {
                    Task task = new Task(async () =>
                    {
                        while(true)
                        {
                            if (_scene.Enabled == false)
                                continue;

                            ball.MoveBallRandomly(_scene.Width, _scene.Height,1);

                            await Task.Delay(5);
                        }
                    });

                    task.Start();
                }
            }
            public override BallLogic CreateBall(int x, int y, int radius)
            {
                return new BallLogic(x, y, radius);
            }

            public override List<BallLogic> GetBalls()
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
}

using Data;

namespace Logic
{
    abstract class AbstractLogicApi
    {
        public abstract void CreateScene(int width, int height, int ballsAmount, int radius);

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
                            ball.MoveBallRandomly(_scene.Width, _scene.Height,1);

                            await Task.Delay(5);
                        }
                    });

                    task.Start();
                }
            }
        }
    }
}

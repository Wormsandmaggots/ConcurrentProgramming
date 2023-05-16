using DataTests;
using Data;
using Logic;
using static System.Formats.Asn1.AsnWriter;


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
            _dataApi = AbstractDataApi.CreateDataApi();
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
            foreach (IBallLogic ballLogic in GetBalls())
            {
                ballLogic.PropertyChanged += Update2;
            }

        }
        public override IBallLogic CreateBall(int x, int y, int radius, int width, int height)
        {
            return new TestBallLogic(_dataApi.CreateBall(x, y, width, height), radius);
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


        private void Update2(object sender)
        {
            IBallLogic ballLogic = (IBallLogic)sender;
            CheckCollision(ballLogic);
        }

        private void CheckCollision(IBallLogic ball)
        {
            BorderCollision(ball);

            if (ball.CanCollide())
            {
                BallColision(ball);
            }

        }

        private void BorderCollision(IBallLogic ball)
        {

            if (ball.X + ball.Radius > _scene.Width)
            {
                if (ball.XVelocity > 0)
                    ball.XVelocity = -ball.XVelocity;
                //może w momencie zmiany prędkości ustawić inną pozycje i wtedy to jest w ball
                //ball.X = _scene.Width - ball.Radius;
            }
            else if (ball.X - ball.Radius < 0)
            {
                if (ball.XVelocity < 0)
                    ball.XVelocity = -ball.XVelocity;

                //ball.X = ball.Radius;
            }


            if (ball.Y + ball.Radius > _scene.Height)
            {
                if (ball.YVelocity > 0)
                    ball.YVelocity = -ball.YVelocity;
                //ball.Y = _scene.Height - ball.Radius;
            }
            else if (ball.Y - ball.Radius < 0)
            {
                if (ball.YVelocity < 0)
                    ball.YVelocity = -ball.YVelocity;

                //ball.Y = ball.Radius;
            }

        }

        private object _lock = new object();

        private void BallColision(IBallLogic ballLogic)
        {
            lock (_lock)
            {
                foreach (IBallLogic checkedBall in GetBalls())
                {
                    if (ballLogic == checkedBall)
                    {
                        continue;
                    }


                    double xGap = ballLogic.X - checkedBall.X;
                    double yGap = ballLogic.Y - checkedBall.Y;

                    double distance = Math.Sqrt((xGap * xGap) + (yGap * yGap)); //wzór na długość wektora między punktami A i B


                    if (Math.Abs(distance) < checkedBall.Radius + ballLogic.Radius)
                    {
                        ballLogic.SetCanCollide(false);
                        checkedBall.SetCanCollide(false);

                        double newVelocityBuffor = ballLogic.XVelocity * ((2 * ballLogic.Weight) / (ballLogic.Weight + ballLogic.Weight)) + checkedBall.XVelocity * ((checkedBall.Weight - ballLogic.Weight) / (ballLogic.Weight + ballLogic.Weight));
                        ballLogic.XVelocity = ballLogic.XVelocity * ((ballLogic.Weight - checkedBall.Weight) / (ballLogic.Weight + ballLogic.Weight)) + checkedBall.XVelocity * ((2 * checkedBall.Weight) / (ballLogic.Weight + ballLogic.Weight));
                        checkedBall.XVelocity = newVelocityBuffor;

                        newVelocityBuffor = ballLogic.YVelocity * ((2 * ballLogic.Weight) / (ballLogic.Weight + ballLogic.Weight)) + checkedBall.YVelocity * ((checkedBall.Weight - ballLogic.Weight) / (ballLogic.Weight + ballLogic.Weight));
                        ballLogic.YVelocity = ballLogic.YVelocity * ((ballLogic.Weight - checkedBall.Weight) / (ballLogic.Weight + ballLogic.Weight)) + checkedBall.YVelocity * ((2 * checkedBall.Weight) / (ballLogic.Weight + ballLogic.Weight));
                        checkedBall.YVelocity = newVelocityBuffor;

                        Action<Object> a = async (Object) =>
                        {

                            await Task.Delay(80);
                            ballLogic.SetCanCollide(true);
                            checkedBall.SetCanCollide(true);
                        };

                        ThreadPool.QueueUserWorkItem(new WaitCallback(a));

                        return;
                    }

                }
            }
        }

    }
}


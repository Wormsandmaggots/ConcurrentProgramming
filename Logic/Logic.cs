using Data;
using Microsoft.VisualBasic;
using System.ComponentModel;

namespace Logic
{
    public abstract class AbstractLogicApi
    {
        public abstract void CreateScene(int width, int height, int ballsAmount, int radius);

        public abstract IBallLogic CreateBall(int x, int y, int radius, int width, int height);
        public abstract List<IBallLogic> GetBalls();
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

            private IScene _scene;

            public static Barrier barrier = new Barrier(2);


            public LogicApi()
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

                _scene = new Scene(width, height);
                _scene.GenerateBallsList(ballsAmount, radius);
              foreach (IBallLogic ballLogic in GetBalls())
                {
                    ballLogic.PropertyChanged += Update2;
                }

            }
            public override IBallLogic CreateBall(int x, int y, int radius, int width, int height)
            {
                return new BallLogic(_dataApi.CreateBall(x, y, radius, width, height));
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
                BorderCollision2(ball);
                BallColision(ball);
            }

            private void BorderCollision2(IBallLogic ball)
            {
                lock(ball)
                {
                    if (ball.X + ball.Radius > _scene.Width)
                    {
                        ball.XVelocity = -ball.XVelocity;
                        ball.X = _scene.Width - ball.Radius;
                    }
                    else if (ball.X - ball.Radius < 0)
                    {
                        ball.XVelocity = -ball.XVelocity;
                        ball.X = ball.Radius;
                    }


                    if (ball.Y + ball.Radius > _scene.Height)
                    {
                        ball.YVelocity = -ball.YVelocity;
                        ball.Y = _scene.Height - ball.Radius;
                    }
                    else if (ball.Y - ball.Radius < 0)
                    {
                        ball.YVelocity = -ball.YVelocity;
                        ball.Y = ball.Radius;
                    }
                }
            }

            private void BorderCollision (IBallLogic ballLogic)
            {
                if ((ballLogic.X + ballLogic.Radius) >= _scene.Width)
                {
                    ballLogic.XVelocity = -ballLogic.XVelocity;
                    ballLogic.X = _scene.Width - ballLogic.Radius;
                }
                if ((ballLogic.X - ballLogic.Radius) <= 0)
                {
                    ballLogic.XVelocity = -ballLogic.XVelocity;
                    ballLogic.X = ballLogic.Radius;
                }
                if ((ballLogic.Y + ballLogic.Radius) >= _scene.Height)
                {
                    ballLogic.YVelocity = -ballLogic.YVelocity;
                    ballLogic.Y = _scene.Height - ballLogic.Radius;
                }
                if ((ballLogic.Y - ballLogic.Radius) <= 0)
                {
                    ballLogic.YVelocity = -ballLogic.YVelocity;
                    ballLogic.Y = ballLogic.Radius;
                }
            }

            private void BallColision( IBallLogic ballLogic)

            { 
 
                foreach( IBallLogic checkedBall in GetBalls())
                {
                    if ( ballLogic == checkedBall)
                    {
                        continue;
                    }

                    
                    double xGap = ballLogic.X + ballLogic.XVelocity - checkedBall.X + checkedBall.XVelocity;
                    double yGap = ballLogic.Y + ballLogic.YVelocity - checkedBall.Y + checkedBall.YVelocity;

                    double distance = Math.Sqrt((xGap * xGap) + (yGap * yGap)); //wzór na długość wektora między punktami A i B


                    if (Math.Abs(distance) <= checkedBall.Radius + ballLogic.Radius)
                    {
                        /*lock (this) {
                            double newVelocityX = ((checkedBall.XVelocity * (checkedBall.Weight - ballLogic.Weight) + (ballLogic.Weight * ballLogic.XVelocity * 2)) / (checkedBall.Weight + ballLogic.Weight));
                            ballLogic.XVelocity = ((ballLogic.XVelocity * (ballLogic.Weight - checkedBall.Weight) + (checkedBall.Weight * checkedBall.XVelocity * 2)) / (checkedBall.Weight + ballLogic.Weight));
                            checkedBall.XVelocity = newVelocityX;

                            double newVelocityY = ((checkedBall.YVelocity * (checkedBall.Weight - ballLogic.Weight)) + (ballLogic.Weight * ballLogic.YVelocity * 2) / (checkedBall.Weight + ballLogic.Weight));
                            ballLogic.YVelocity = ((ballLogic.YVelocity * (ballLogic.Weight - checkedBall.Weight)) + (checkedBall.Weight * checkedBall.YVelocity * 2) / (checkedBall.Weight + ballLogic.Weight));
                            checkedBall.YVelocity = newVelocityY;

                        } */

                        // barrier.SignalAndWait();

                        lock (this)
                        {
                            double newVelocityBuffor = ballLogic.XVelocity * ((2 * ballLogic.Weight) / (ballLogic.Weight + ballLogic.Weight)) + checkedBall.XVelocity * ((checkedBall.Weight - ballLogic.Weight) / (ballLogic.Weight + ballLogic.Weight));
                            ballLogic.XVelocity = ballLogic.XVelocity * ((ballLogic.Weight - checkedBall.Weight) / (ballLogic.Weight + ballLogic.Weight)) + checkedBall.XVelocity * ((2 * checkedBall.Weight) / (ballLogic.Weight + ballLogic.Weight));
                            checkedBall.XVelocity = newVelocityBuffor;

                            newVelocityBuffor = ballLogic.YVelocity * ((2 * ballLogic.Weight) / (ballLogic.Weight + ballLogic.Weight)) + checkedBall.YVelocity * ((checkedBall.Weight - ballLogic.Weight) / (ballLogic.Weight + ballLogic.Weight));
                            ballLogic.YVelocity = ballLogic.YVelocity * ((ballLogic.Weight - checkedBall.Weight) / (ballLogic.Weight + ballLogic.Weight)) + checkedBall.YVelocity * ((2 * checkedBall.Weight) / (ballLogic.Weight + ballLogic.Weight));
                            checkedBall.YVelocity = newVelocityBuffor;

                        }
                    }

                }
            }

        }
    }
}

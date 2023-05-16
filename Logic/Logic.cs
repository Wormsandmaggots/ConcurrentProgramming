using Data;
using Microsoft.VisualBasic;
using System.ComponentModel;
using System.Numerics;

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
                return new BallLogic(_dataApi.CreateBall(x, y, width, height), radius);
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


           private void Update2(IBallLogic ballLogic)
            {
                CheckCollision(ballLogic);
            }

            private void CheckCollision(IBallLogic ball)
            {
                BorderCollision(ball);

                if(ball.CanCollide())
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
                }
                else if (ball.X - ball.Radius < 0)
                {
                    if (ball.XVelocity < 0)
                        ball.XVelocity = -ball.XVelocity;
                }


                if (ball.Y + ball.Radius > _scene.Height)
                {
                    if (ball.YVelocity > 0)
                        ball.YVelocity = -ball.YVelocity;
                }
                else if (ball.Y - ball.Radius < 0)
                {
                    if (ball.YVelocity < 0)
                        ball.YVelocity = -ball.YVelocity;
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
                           
                            double newVelocityBuffor = ballLogic.XVelocity * ((2 * ballLogic.Weight) / (ballLogic.Weight + checkedBall.Weight)) + checkedBall.XVelocity * ((checkedBall.Weight - ballLogic.Weight) / (ballLogic.Weight + checkedBall.Weight));
                            ballLogic.XVelocity = ballLogic.XVelocity * ((ballLogic.Weight - checkedBall.Weight) / (ballLogic.Weight + checkedBall.Weight)) + checkedBall.XVelocity * ((2 * checkedBall.Weight) / (ballLogic.Weight + checkedBall.Weight));
                            checkedBall.XVelocity = newVelocityBuffor;

                            newVelocityBuffor = ballLogic.YVelocity * ((2 * ballLogic.Weight) / (ballLogic.Weight + checkedBall.Weight)) + checkedBall.YVelocity * ((checkedBall.Weight - ballLogic.Weight) / (ballLogic.Weight + checkedBall.Weight));
                            ballLogic.YVelocity = ballLogic.YVelocity * ((ballLogic.Weight - checkedBall.Weight) / (ballLogic.Weight + checkedBall.Weight)) + checkedBall.YVelocity * ((2 * checkedBall.Weight) / (ballLogic.Weight + checkedBall.Weight));
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
}

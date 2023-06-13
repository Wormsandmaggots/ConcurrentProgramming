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


            private object _lock = new object();



            private void Update2(IBallLogic ballLogic)
            {
                lock(_lock) { 

                    CheckCollision(ballLogic);
                }
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
                Vector2 pos = ball.Position;
                Vector2 vel = ball.Velocity;

                bool hasChanged = false;

                if (pos.X + ball.Radius > _scene.Width)
                {
                    if (vel.X > 0)
                    {
                        vel.X = -vel.X;
                        hasChanged = true;
                    }
                        
                }
                else if (pos.X - ball.Radius < 0)
                {
                    if (vel.X < 0)
                    {
                        vel.X = -vel.X;
                        hasChanged = true;
                    }
                }


                if (pos.Y + ball.Radius > _scene.Height)
                {
                    if (vel.Y > 0)
                    {
                        vel.Y = -vel.Y;
                        hasChanged = true;
                    }
                        
                }
                else if (pos.Y - ball.Radius < 0)
                {
                    if (vel.Y < 0)
                    {
                        vel.Y = -vel.Y;
                        hasChanged = true;
                    }
                        
                }

                if(hasChanged) 
                {
                    ball.Velocity = vel;
                }
                
            }

            private void BallColision(IBallLogic ballLogic)
            {
                ///lock (_lock)
                //{
                Vector2 pos = ballLogic.Position;
                Vector2 vel = ballLogic.Velocity;

                foreach (IBallLogic checkedBall in GetBalls())
                {
                    if (ballLogic == checkedBall)
                    {
                        continue;
                    }

                    Vector2 checkedPos = checkedBall.Position;
                    Vector2 checkedVel = checkedBall.Velocity;


                    double xGap = pos.X - checkedPos.X;
                    double yGap = pos.Y - checkedPos.Y;

                    double distance = Math.Sqrt((xGap * xGap) + (yGap * yGap)); //wzór na długość wektora między punktami A i B


                    if (Math.Abs(distance) < checkedBall.Radius + ballLogic.Radius)
                    {
                            
                        ballLogic.SetCanCollide(false);
                        checkedBall.SetCanCollide(false);
                           
                        double newCheckedXVel = ((checkedVel.X * (checkedBall.Weight - ballLogic.Weight) + (ballLogic.Weight * vel.X * 2)) / (checkedBall.Weight + ballLogic.Weight));
                        double newBallLogicXVel = ((vel.X * (ballLogic.Weight - checkedBall.Weight) + (checkedBall.Weight * checkedVel.X * 2)) / (checkedBall.Weight + ballLogic.Weight));

                        double newCheckedYVel = ((checkedVel.Y * (checkedBall.Weight - ballLogic.Weight)) + (ballLogic.Weight * vel.Y * 2) / (checkedBall.Weight + ballLogic.Weight));
                        double newBallLogicYVel = ((vel.Y * (ballLogic.Weight - checkedBall.Weight)) + (checkedBall.Weight * checkedVel.Y * 2) / (checkedBall.Weight + ballLogic.Weight));

                        ballLogic.Velocity = new Vector2((float)newBallLogicXVel, (float)newBallLogicYVel);
                        checkedBall.Velocity = new Vector2((float)newCheckedXVel, (float)newCheckedYVel);

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
               // }
            }

        }
    }
}

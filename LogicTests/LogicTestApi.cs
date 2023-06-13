using DataTests;
using Data;
using Logic;
using static System.Formats.Asn1.AsnWriter;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;

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

        public void CheckCollision(IBallLogic ball)
        {
            BorderCollision(ball);

            if (ball.CanCollide())
            {
                BallColision(ball);
            }

        }

        public void BorderCollision(IBallLogic ball)
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

            if (hasChanged)
            {
                ball.Velocity = vel;
            }

        }

        public void BallColision(IBallLogic ballLogic)
        {
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

                    ballLogic.SetCanCollide(true);
                    checkedBall.SetCanCollide(true);
                   
                  return;
                }

            }
        }

        private int _weigth = 1;
        public List<Vector2> newVelocity(Vector2 vel, Vector2 pos, Vector2 checkedVel, Vector2 checkedPos)
        {
            List<Vector2> velocities = new List<Vector2>();
            int _weight = 1;

            double newCheckedXVel = ((checkedVel.X * (_weight - _weight) + (_weight * vel.X * 2)) / (_weight + _weight));
            double newBallLogicXVel = ((vel.X * (_weight - _weight) + (_weight * checkedVel.X * 2)) / (_weight + _weight));

            double newCheckedYVel = ((checkedVel.Y * (_weight - _weight)) + (_weight * vel.Y * 2) / (_weight + _weight));
            double newBallLogicYVel = ((vel.Y * (_weight - _weight)) + (_weight * checkedVel.Y * 2) / (_weight + _weight));

            Vector2 ballVelocity = new Vector2((float)newBallLogicXVel, (float)newBallLogicYVel);
            Vector2 ball2Velocity = new Vector2((float)newCheckedXVel, (float)newCheckedYVel);

            velocities.Add(ballVelocity);
            velocities.Add(ball2Velocity);
            return velocities;
        }
    }
}

           




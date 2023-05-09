﻿using Data;
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
              //  foreach (IBallLogic ballLogic in GetBalls())
                //{
               //     ballLogic.PropertyChanged += Update;
              //  }

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


          /*  private void Update(object sender, PropertyChangedEventArgs ev)
            {
                IBallLogic ballLogic = (IBallLogic)sender;
                if (ev.PropertyName == "Position")
                {
                    // CheckCollision(orb);
                }

            }
          */


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

        }
    }
}

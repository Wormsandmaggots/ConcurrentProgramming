using System;

namespace Data
{
    internal class Ball : IBall
    {
        private double _x, _y;
        private int _radius;
        private int _weight; //TODO:: w razie czego zmienić na double
        private double[] _velocity = new double[2];
        private bool _canMove;

        public event Action PropertyChanged;

        public Ball(int x, int y, int radius, int width, int height)
        {
            _x = x;
            _y = y;
            _radius = radius;
            Random random = new Random();
            _weight = 1;
            double yVelocity = random.NextDouble()*0.99;
            double xVelocity = random.NextDouble()*0.99;
            yVelocity = (random.Next(-1, 1) < 0) ? yVelocity : -yVelocity;
            this._velocity[0] = xVelocity;
            this._velocity[1] = yVelocity;

            //_move = new Task(async () =>
            //{
            //    while (true)
            //    {
            //        MoveBallRandomly(width, height, 1);

            //        await Task.Delay(5);

            //        if (_canMove == false) return;
            //    }
            //});

            //_move.Start();

            Action<Object> move = async void (Object state) =>
            {
                while (true)
                {
                    MoveBallRandomly(width, height, xVelocity, yVelocity);

                    await Task.Delay(5);

                    if (_canMove == false) return;
                }
            };


            ThreadPool.QueueUserWorkItem(new WaitCallback(move));
        }

        public void MoveBallRandomly(int xBorder, int yBorder, double xVelocity, double yVelocity)
        {
            Random r = new Random();

              double x = this._x;
              double y = this._y;

             /* do
              {
                  y = r.Next(-1, 2);

              } while (x == 0 && y == 0);*/

              x += xVelocity;
              y += yVelocity;

              if (X + x + Radius > xBorder)
              {
                  X = xBorder - Radius;
              }
              else if (X + x - Radius < 0)
              {
                  X = Radius;
              }
              else
              {
                  X = x;
              }


              if (Y + y + Radius > yBorder)
              {
                  Y = yBorder - Radius;
              }
              else if (Y + y - Radius < 0)
              {
                  Y = Radius;
              }
              else
              {
                  Y = y;
              }
        }

        public void OnPropertyChanged()
        {
            PropertyChanged?.Invoke();
        }

        public void ToggleBall(bool val)
        {
            _canMove = val;
        }

        public void Dispose()
        {
            //_move.Dispose();
        }

        public double X
        {
            get { return _x; }
            set
            {
                _x = value;
                OnPropertyChanged();
            }
        }

        public double Y
        {
            get { return _y; }
            set
            {
                _y = value;
                OnPropertyChanged();
            }
        }

        public double XVelocity
        {
            get { return _velocity[0]; }
            set
            {
                _velocity[0] = value;
                OnPropertyChanged();
            }
        }

        public double YVelocity
        {
            get { return _velocity[1]; }
            set
            {
                _velocity[2] = value;
                OnPropertyChanged();
            }
        }

        public int Radius => _radius;
    }
}
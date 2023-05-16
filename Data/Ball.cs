using System;

namespace Data
{
    internal class Ball : IBall
    {
        private double _x, _y;
        private double[] _velocity = new double[2];
        private bool _canMove = true;
        private float _initialDelay = 20f;
        private int _delay;
        private object _lock = new object();

        public event Action PropertyChanged;

        public Ball(int x, int y, int width, int height)
        {
            _x = x;
            _y = y;
            Random random = new Random();
            double yVelocity = random.NextDouble() * 4.5f;
            double xVelocity = random.NextDouble() * 4.5f;
            xVelocity = (random.Next(-1, 1) < 0) ? xVelocity : -xVelocity;
            yVelocity = (random.Next(-1, 1) < 0) ? yVelocity : -yVelocity;
            this._velocity[0] = xVelocity;
            this._velocity[1] = yVelocity;


            Action<Object> move = async void (Object state) =>
            {
                while (true)
                {
                    
                    MoveBallRandomly(width, height, _velocity[0], _velocity[1]);

                    lock(_lock)
                    {
                        _delay = (int)(_initialDelay/Math.Sqrt(xVelocity * xVelocity + yVelocity * yVelocity));
                    }

                    await Task.Delay(_delay);

                    if (_canMove == false) return;
                }
            };


            ThreadPool.QueueUserWorkItem(new WaitCallback(move));
        }

        public void MoveBallRandomly(int xBorder, int yBorder, double xVelocity, double yVelocity)
        {
              double x = this._x;
              double y = this._y;

              x += xVelocity;
              y += yVelocity;

            X = x;
            Y = y;
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

            private set
            {
                _x = value;
                OnPropertyChanged();
            }
        }

        public double Y
        {
            get { return _y; }

            private set
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
            }
        }

        public double YVelocity
        {
            get { return _velocity[1]; }
            set
            {
                _velocity[1] = value;
            }
        }

    }
}
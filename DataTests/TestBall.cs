using Data;

namespace DataTests
{
    internal class TestBall : IBall
    {
        private double _x, _y;
        private double[] _velocity = new double[2];
        private bool _canMove = true;

        public event Action PropertyChanged;

        public TestBall(int x, int y, int width, int height)
        {
            _x = x;
            _y = y;
            Random random = new Random();
            double yVelocity = random.NextDouble() * 4.5;
            double xVelocity = random.NextDouble() * 4.5;
            xVelocity = (random.Next(-1, 1) < 0) ? xVelocity : -xVelocity;
            yVelocity = (random.Next(-1, 1) < 0) ? yVelocity : -yVelocity;
            this._velocity[0] = xVelocity;
            this._velocity[1] = yVelocity;


            Action<Object> move = async void (Object state) =>
            {
                while (true)
                {
                    lock (this)
                    {
                        MoveBallRandomly(width, height, _velocity[0], _velocity[1]);
                    }

                    await Task.Delay(5);

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

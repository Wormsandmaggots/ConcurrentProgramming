using Data;

namespace DataTests
{
    internal class TestBall : IBall
    {
        private int _x, _y;
        private int _radius;
        private bool _canMove;

        public event Action PropertyChanged;

        public TestBall(int x, int y, int radius, int width, int height)
        {
            _x = x;
            _y = y;
            _radius = radius;

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
                    MoveBallRandomly(width, height, 1);

                    await Task.Delay(5);

                    if (_canMove == false) return;
                }
            };

            ThreadPool.QueueUserWorkItem(new WaitCallback(move));
        }

        public void MoveBallRandomly(int xBorder, int yBorder, int moveDistance)
        {
            Random r = new Random();

            int x = r.Next(-1, 2);
            int y;

            do
            {
                y = r.Next(-1, 2);

            } while (x == 0 && y == 0);

            x *= moveDistance;
            y *= moveDistance;

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
                X = X + x;
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
                Y = Y + y;
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

        public void MoveBallRandomly(int xBorder, int yBorder, double xVelocity, double yVelocity)
        {
            throw new NotImplementedException();
        }

        public int X
        {
            get { return _x; }
            set
            {
                _x = value;
                OnPropertyChanged();
            }
        }

        public int Y
        {
            get { return _y; }
            set
            {
                _y = value;
                OnPropertyChanged();
            }
        }

        public int Radius => _radius;

        double IBall.X { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        double IBall.Y { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double XVelocity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double YVelocity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int Weight => throw new NotImplementedException();
    }
}

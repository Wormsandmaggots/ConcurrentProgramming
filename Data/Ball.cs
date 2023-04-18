using System.ComponentModel;

namespace Data
{
    internal class Ball : IBall
    {
        private int _x, _y;
        private int _radius;
        private bool _canMove;
        private Task _move;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Ball(int x, int y, int radius, int width, int height)
        {
            _x = x;
            _y = y;
            _radius = radius;

            _move = new Task(async () =>
            {
                while (true)
                {
                    MoveBallRandomly(width, height, 1);

                    await Task.Delay(5);

                    if (_canMove == false) return;
                }
            });

            _move.Start();
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

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ToggleBall(bool val)
        {
            _canMove = val;
        }

        public void Dispose()
        {
            _move.Dispose();
        }

        public int X
        {
            get { return _x; }
            set
            {
                _x = value;
                OnPropertyChanged("X");
            }
        }

        public int Y
        {
            get { return _y; }
            set
            {
                _y = value;
                OnPropertyChanged("Y");
            }
        }

        public int Radius => _radius;
    }
}
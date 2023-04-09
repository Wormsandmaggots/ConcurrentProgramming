using System.ComponentModel;
using Data;

namespace Logic
{
    public class BallLogic : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Ball _ball;


        public BallLogic(int x, int y, int radius)
        {
            _ball = new Ball(x, y, radius);
            _ball.PropertyChanged += Update;
        }

        public BallLogic(Ball ball)
        {
            _ball = ball;
            _ball.PropertyChanged += Update;
        }

        private void Update(object source, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "X")
            {
                OnPropertyChanged("X");
            }
            else if (e.PropertyName == "Y")
            {
                OnPropertyChanged("Y");
            }
        }

        public void MoveBallRandomly(int xBorder, int yBorder, int moveDistance)
        {
            Random r = new Random();
            int[] tempArr = new int[] {-1, 0, 1};

            int x = r.Next(-1, 2);
            int y;

            do
            {
                y = r.Next(-1, 2);

            } while (x == 0 && y == 0);

            x *= moveDistance;
            y *= moveDistance;

            if (_ball.X + x + _ball.Radius > xBorder)
            {
                _ball.X = xBorder - _ball.Radius;
            }
            else if (_ball.X + x - _ball.Radius < 0)
            {
                _ball.X = _ball.Radius;
            }
            else
            {
                _ball.X += x;
            }


            if (_ball.Y + y + _ball.Radius > yBorder)
            {
                _ball.Y = yBorder - _ball.Radius;
            }
            else if (_ball.Y + y - _ball.Radius < 0)
            {
                _ball.Y = _ball.Radius;
            }
            else
            {
                _ball.Y += y;
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int X
        {
            get { return _ball.X; }
            set
            {
                _ball.X = value;
                OnPropertyChanged("X");
            }
        }

        public int Y
        {
            get { return _ball.Y; }
            set
            {
                _ball.Y = value;
                OnPropertyChanged("Y");
            }
        }

        public int Radius
        {
            get { return _ball.Radius; }
        }
    }
}

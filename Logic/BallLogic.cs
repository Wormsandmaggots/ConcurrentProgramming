using System.ComponentModel;
using Data;

namespace Logic
{
    public class BallLogic : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private IBall _ball;


        //public BallLogic(int x, int y, int radius)
        //{
        //    _ball = new IBall(x, y, radius);
        //    _ball.PropertyChanged += Update;
        //}

        public BallLogic(IBall ball)
        {
            _ball = ball;
            _ball.PropertyChanged += Update;
        }

        ~BallLogic()
        {
            PropertyChanged = null;
            _ball.PropertyChanged -= Update;
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
            }
        }

        public int Y
        {
            get { return _ball.Y; }
            set
            {
                _ball.Y = value;
            }
        }

        public int Radius
        {
            get { return _ball.Radius; }
        }
    }
}

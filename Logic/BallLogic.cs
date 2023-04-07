using System;
using System.ComponentModel;
using Data;

namespace Logic
{
    internal class BallLogic : INotifyPropertyChanged
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

            int x = r.Next(-moveDistance, moveDistance);
            int y = r.Next(-moveDistance, moveDistance);

            if (_ball.X + x + _ball.Radius > xBorder)
            {
                _ball.X = xBorder - _ball.Radius;
            }
            else if (_ball.X + x - _ball.Radius < xBorder)
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
            else if (_ball.Y + y - _ball.Radius < yBorder)
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
    }
}

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
                OnPropertyChanged("Y");
            }
        }

        public int Y
        {
            get { return _ball.Y; }
            set
            {
                _ball.Y = value;
                OnPropertyChanged("X");
            }
        }
    }
}

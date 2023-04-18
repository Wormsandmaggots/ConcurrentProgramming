using System.ComponentModel;
using Data;

namespace Logic
{
    internal class BallLogic : IBallLogic
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private IBall _ball;

        public BallLogic(IBall ball)
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

        public void ToggleBall(bool val)
        {
            _ball.ToggleBall(val);
        }

        public void Dispose()
        {
            _ball.PropertyChanged -= Update;
            PropertyChanged = (PropertyChangedEventHandler)Delegate.RemoveAll(PropertyChanged, PropertyChanged);
            _ball.Dispose();
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

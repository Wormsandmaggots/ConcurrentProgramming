using System.ComponentModel;

namespace Data
{
    internal class Ball : IBall
    {
        private int _x, _y;
        private int _radius;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Ball(int x, int y, int radius)
        {
            _x = x;
            _y = y;
            _radius = radius;
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
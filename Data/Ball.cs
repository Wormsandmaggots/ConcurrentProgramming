using System.ComponentModel;

namespace Data
{
    public class Ball : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _x, _y;
        private int _radius;

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
                OnPropertyChanged("Y");
            }
        }

        public int Y
        {
            get { return _y; }
            set
            {
                _y = value;
                OnPropertyChanged("X");
            }
        }
    }
}
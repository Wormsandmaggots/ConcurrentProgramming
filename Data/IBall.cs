using System.ComponentModel;

namespace Data
{
    public interface IBall : INotifyPropertyChanged
    {
        abstract int X { get; set; }
        abstract int Y { get; set; }
        abstract int Radius { get; }

        event PropertyChangedEventHandler? PropertyChanged;

        abstract void MoveBallRandomly(int xBorder, int yBorder, int moveDistance);

        abstract void ToggleBall(bool val);
    }
}

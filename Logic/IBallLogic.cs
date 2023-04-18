using System.ComponentModel;

namespace Logic
{
    public interface IBallLogic : INotifyPropertyChanged
    {
        abstract int X { get; set; }
        abstract int Y { get; set; }
        abstract int Radius { get; }

        event PropertyChangedEventHandler? PropertyChanged;
        abstract void ToggleBall(bool val);
    }
}

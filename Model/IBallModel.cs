using System.ComponentModel;

namespace Model
{
    public interface IBallModel : INotifyPropertyChanged, IDisposable
    {
        abstract double XHandler { get; set; }
        abstract double YHandler { get; set; }
        abstract int RadiusHandler { get; }

        event PropertyChangedEventHandler? PropertyChanged;

    }
}


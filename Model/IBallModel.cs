using System.ComponentModel;

namespace Model
{
    public interface IBallModel : INotifyPropertyChanged, IDisposable
    {
        abstract int XHandler { get; set; }
        abstract int YHandler { get; set; }
        abstract int RadiusHandler { get; }

        event PropertyChangedEventHandler? PropertyChanged;

    }
}


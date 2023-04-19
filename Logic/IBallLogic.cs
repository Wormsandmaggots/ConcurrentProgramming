namespace Logic
{
    public interface IBallLogic : IDisposable
    {
        abstract int X { get; set; }
        abstract int Y { get; set; }
        abstract int Radius { get; }

        event Action<Object> PropertyChanged;
        abstract void ToggleBall(bool val);
    }
}

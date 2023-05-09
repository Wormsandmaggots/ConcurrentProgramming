namespace Logic
{
    public interface IBallLogic : IDisposable
    {
        abstract double X { get; set; }
        abstract double Y { get; set; }
        abstract int Radius { get; }

        event Action<Object> PropertyChanged;
        abstract void ToggleBall(bool val);
    }
}

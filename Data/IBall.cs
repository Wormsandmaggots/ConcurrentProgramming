namespace Data
{
    public interface IBall : IDisposable
    {
        abstract int X { get; set; }
        abstract int Y { get; set; }
        abstract int Radius { get; }

        event Action PropertyChanged;

        abstract void MoveBallRandomly(int xBorder, int yBorder, int moveDistance);

        abstract void ToggleBall(bool val);
    }
}

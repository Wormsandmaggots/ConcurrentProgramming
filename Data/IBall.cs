namespace Data
{
    public interface IBall : IDisposable
    {
        abstract double X { get; set; }
        abstract double Y { get; set; }
        abstract int Radius { get; }

        event Action PropertyChanged;

        abstract void MoveBallRandomly(int xBorder, int yBorder, double xVelocity, double yVelocity);

        abstract void ToggleBall(bool val);
    }
}

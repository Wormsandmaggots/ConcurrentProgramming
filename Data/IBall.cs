namespace Data
{
    public interface IBall : IDisposable
    {
        abstract double X { get; }
        abstract double Y { get; }
        abstract double XVelocity { get; set; }
        abstract double YVelocity { get; set; }

        event Action PropertyChanged;

        abstract void MoveBall(int xBorder, int yBorder, double xVelocity, double yVelocity);

        abstract void ToggleBall(bool val);
    }
}

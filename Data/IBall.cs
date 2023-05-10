namespace Data
{
    public interface IBall : IDisposable
    {
        //zmienić settery na prywatne
        abstract double X { get; }
        abstract double Y { get; }
        abstract int Radius { get; }
        abstract int Weight { get; }
        abstract double XVelocity { get; set; }
        abstract double YVelocity { get; set; }

        event Action PropertyChanged;

        abstract void MoveBallRandomly(int xBorder, int yBorder, double xVelocity, double yVelocity);

        abstract void ToggleBall(bool val);
    }
}

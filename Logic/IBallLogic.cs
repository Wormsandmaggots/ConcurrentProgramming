namespace Logic
{
    public interface IBallLogic : IDisposable
    {
        abstract double X { get; }
        abstract double Y { get; }
        abstract bool CanCollide();
        abstract void SetCanCollide(bool canCollide);
        abstract int Radius { get; }
        abstract int Weight { get; }
        abstract double XVelocity { get; set; }
        abstract double YVelocity { get; set; }

        event Action<IBallLogic> PropertyChanged;
        abstract void ToggleBall(bool val);
    }
}

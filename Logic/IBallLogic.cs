namespace Logic
{
    public interface IBallLogic : IDisposable
    {
        abstract double X { get; set; }
        abstract double Y { get; set; }
        abstract bool CanCollide();
        abstract void SetCanCollide(bool canCollide);
        abstract int Radius { get; }
        abstract int Weight { get; }
        abstract double XVelocity { get; set; }
        abstract double YVelocity { get; set; }

        event Action<Object> PropertyChanged;
        abstract void ToggleBall(bool val);
    }
}

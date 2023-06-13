using Data;
using System.Numerics;

namespace Logic
{
    public interface IBallLogic : IDisposable
    {
        abstract Vector2 Position { get; }
        abstract Vector2 Velocity { get; set; }
        abstract bool CanCollide();
        abstract IBall GetBall();
        abstract void SetCanCollide(bool canCollide);
        abstract int Radius { get; }
        abstract int Weight { get; }

        event Action<IBallLogic> PropertyChanged;
        abstract void ToggleBall(bool val);
    }
}

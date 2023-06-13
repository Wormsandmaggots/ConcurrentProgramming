using System.Numerics;

namespace Data
{
    public interface IBall : IDisposable
    {
        abstract Vector2 Position { get;}
        abstract Vector2 Velocity { get; set; }
        

        event Action PropertyChanged;

        abstract void MoveBall(int xBorder, int yBorder, Vector2 velocity);

        abstract void ToggleBall(bool val);
    }
}

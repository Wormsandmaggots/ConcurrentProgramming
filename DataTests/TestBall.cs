using Data;
using System.Numerics;

namespace DataTests
{
    internal class TestBall : IBall
    {
        private Vector2 pos;
        private Vector2 _velocity;
        private bool _canMove = true;
        private float _initialDelay = 20f;
        private int _delay;
        private object _lock = new object();

        public event Action PropertyChanged;

        public TestBall(int x, int y, int width, int height)
        {
            pos = new Vector2(x, y);
            Random random = new Random();
            float yVelocity = (float)random.NextDouble() * 4.5f;
            float xVelocity = (float)random.NextDouble() * 4.5f;
            xVelocity = (random.Next(-1, 1) < 0) ? xVelocity : -xVelocity;
            yVelocity = (random.Next(-1, 1) < 0) ? yVelocity : -yVelocity;

            _velocity = new Vector2(xVelocity, yVelocity);
        }

        public void MoveBall(int xBorder, int yBorder, Vector2 velocity)
        {
            float x = pos.X;
            float y = pos.Y;

            x += velocity.X;
            y += velocity.Y;

            Position = new Vector2(x, y);
        }

        public void OnPropertyChanged()
        {
            PropertyChanged?.Invoke();
        }

        public void ToggleBall(bool val)
        {
            _canMove = val;
        }

        public void Dispose()
        {
            //_move.Dispose();
        }

        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public Vector2 Position
        {
            get { return pos; }

            private set
            {
                pos = value;
                OnPropertyChanged();
            }
        }

    }
}

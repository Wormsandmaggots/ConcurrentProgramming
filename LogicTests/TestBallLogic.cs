using Data;
using System.Numerics;

namespace LogicTests
{
    internal class TestBallLogic : IBallLogic
    {
        public event Action<IBallLogic> PropertyChanged;
        private bool _canCollide = true;
        private int _radius;
        private int _weight = 1;

        private IBall _ball;

        public TestBallLogic(IBall ball, int radius)
        {
            _ball = ball;
            _radius = radius;
            _ball.PropertyChanged += Update;
        }

        private void Update()
        {
            OnPropertyChanged();
        }

        private void OnPropertyChanged()
        {
            PropertyChanged?.Invoke(this);
        }

        public void ToggleBall(bool val)
        {
            _ball.ToggleBall(val);
        }

        public void Dispose()
        {
            _ball.PropertyChanged -= Update;
            PropertyChanged = (Action<Object>)Delegate.RemoveAll(PropertyChanged, PropertyChanged);
            _ball.Dispose();
        }

        public bool CanCollide()
        {
            return _canCollide;
        }

        public void SetCanCollide(bool canCollide)
        {
            _canCollide = canCollide;
        }

        public IBall GetBall()
        {
            return _ball;
        }

        public Vector2 Position => _ball.Position;

        public Vector2 Velocity
        {
            get { return _ball.Velocity; }
            set { _ball.Velocity = value; }
        }

        public int Radius
        {
            get { return _radius; }
        }

        public int Weight
        {
            get { return _weight; }
        }
    }
}

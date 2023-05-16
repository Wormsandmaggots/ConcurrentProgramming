using Data;

namespace Logic
{
    internal class BallLogic : IBallLogic
    {
        public event Action<IBallLogic> PropertyChanged;
        private bool _canCollide = true;
        private int _radius;
        private int _weight = 1;

        private IBall _ball;

        public BallLogic(IBall ball, int radius)
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



        public double X
        {
            get { return _ball.X; }
        }

        public double Y
        {
            get { return _ball.Y; }
        }

        public double XVelocity
        {
            get { return _ball.XVelocity; }
            set
            {
                
                _ball.XVelocity = value;

            }
        }

        public double YVelocity
        {
            get { return _ball.YVelocity; }
            set
            {
                _ball.YVelocity = value;
            }
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

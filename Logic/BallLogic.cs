﻿using Data;

namespace Logic
{
    internal class BallLogic : IBallLogic
    {
        public event Action<Object> PropertyChanged;

        private IBall _ball;

        public BallLogic(IBall ball)
        {
            _ball = ball;
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

        public double X
        {
            get { return _ball.X; }
            set
            {
                _ball.X = value;
                OnPropertyChanged();

            }
        }

        public double Y
        {
            get { return _ball.Y; }
            set
            {
                _ball.Y = value;
                OnPropertyChanged();
            }
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
            get { return _ball.Radius; }
        }
    }
}

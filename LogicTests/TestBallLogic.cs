﻿using Data;

namespace LogicTests
{
    internal class TestBallLogic : IBallLogic
    {
        public event Action<Object> PropertyChanged;

        private IBall _ball;

        public TestBallLogic(IBall ball)
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

        public int X
        {
            get { return _ball.X; }
            set
            {
                _ball.X = value;
            }
        }

        public int Y
        {
            get { return _ball.Y; }
            set
            {
                _ball.Y = value;
            }
        }

        public int Radius
        {
            get { return _ball.Radius; }
        }
    }
}
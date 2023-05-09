﻿using Logic;
using Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace ModelTest
{
    internal class BallTestModel : IBallModel
    {
        public double x;
        public double y;
        public int radius;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }

        public BallTestModel(IBallLogic ball)
        {
            this.x = ball.X;
            this.y = ball.Y;
            this.radius = ball.Radius;
            ball.PropertyChanged += Update;
        }

        public void Update(object source)
        {
            IBallLogic toUpdateBall = (IBallLogic)source;

            this.XHandler = toUpdateBall.X;
            this.YHandler = toUpdateBall.Y;
        }

        public void Dispose()
        {
            PropertyChanged = (PropertyChangedEventHandler)Delegate.RemoveAll(PropertyChanged, PropertyChanged);
        }

        public double XHandler
        {
            get { return x; }
            set
            {
                x = value;
                OnPropertyChanged("XHandler");
            }
        }

        public double YHandler
        {
            get { return y; }
            set
            {
                y = value;
                OnPropertyChanged("YHandler");
            }
        }

        public int RadiusHandler
        {
            get { return radius; } //no setter cause it's hardocded
        }

        double IBallModel.XHandler { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        double IBallModel.YHandler { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}

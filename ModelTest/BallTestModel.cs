using Logic;
using Model;
using System.ComponentModel;
using System.Numerics;
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
            Vector2 pos = ball.Position;
            this.x = pos.X;
            this.y = pos.Y;
            this.radius = ball.Radius;
            ball.PropertyChanged += Update;
        }

        public void Update(object source)
        {
            IBallLogic toUpdateBall = (IBallLogic)source;

            Vector2 pos = toUpdateBall.Position;
            this.XHandler = pos.X;
            this.YHandler = pos.Y;
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


    }
}

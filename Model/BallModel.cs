using System.ComponentModel;
using System.Runtime.CompilerServices;
using Logic;

namespace Model
{
    internal class BallModel : IBallModel
    {
        public double x;
        public double y;
        public int radius;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }

        public BallModel(IBallLogic ball)
        {
            this.x = ball.X;
            this.y = ball.Y;
            this.radius = ball.Radius;
            ball.PropertyChanged += Update;
        }

        public void Update (object source)
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


    }
}
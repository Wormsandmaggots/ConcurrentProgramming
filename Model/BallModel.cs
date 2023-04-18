using System.ComponentModel;
using System.Runtime.CompilerServices;
using Logic;

namespace Model
{
    internal class BallModel : IBallModel
    {
        public int x;
        public int y;
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

        public void Update (object source, PropertyChangedEventArgs eventCh)
        {
            IBallLogic toUpdateBall = (IBallLogic)source;

            if(eventCh.PropertyName == "X")
            {
                this.XHandler = toUpdateBall.X;
            }
            else if(eventCh.PropertyName == "Y")
            {
                this.YHandler = toUpdateBall.Y;
            }

        }

        ~BallModel()
        {
            PropertyChanged = null;
        }

        public int XHandler
        {
            get { return x; }
            set
            {
                x = value;
                OnPropertyChanged("XHandler");
            }
        }

        public int YHandler
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
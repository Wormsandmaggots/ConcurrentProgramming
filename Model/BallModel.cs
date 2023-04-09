using System.ComponentModel;
using System.Runtime.CompilerServices;
using Logic;

namespace Model
{
    public class BallModel : INotifyPropertyChanged
    {
        public int x;
        public int y;
        public int radius;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public BallModel(BallLogic ball)
        {
            this.x = ball.X;
            this.y = ball.Y;
            this.radius = ball.Radius;
            ball.PropertyChanged += Update;
        }

        public void Update (object source, PropertyChangedEventArgs eventCh)
        {
            BallLogic toUpdateBall = (BallLogic)source;

            if(eventCh.PropertyName == "X")
            {
                this.XHandler = toUpdateBall.X;
            }
            else if(eventCh.PropertyName == "Y")
            {
                this.YHandler = toUpdateBall.Y;
            }

        }


        public int XHandler
        {
            get { return x; }
            set
            {
                x = value;
                OnPropertyChanged("x");
            }
        }

        public int YHandler
        {
            get { return y; }
            set
            {
                y = value;
                OnPropertyChanged("y");
            }
        }

        public int RadiusHandler
        {
            get { return radius; } //no setter cause it's hardocded
        }

    }
}
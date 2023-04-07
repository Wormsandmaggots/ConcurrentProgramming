using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using Logic;

namespace Model
{
    public /*internal*/ class BallModel : INotifyPropertyChanged
    {
        public int x;
        public int y;
        //Idk if we need here radius, if it's hardcoded.
        public int radius;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void WhenPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public BallModel(BallLogic ball)
        {
            this.x = ball.x;
            this.y = ball.y;
            this.radius = ball.radius;
        }

        public void Update (object source, int valueToMove, PropertyChangedEventArgs eventCh)
        {
            BallLogic toUpdateBall = (BallLogic)source;

            if(eventCh.PropertyName == "x")
            {
                
                this.x = toUpdateBall.x + valueToMove; 
            }
            if(eventCh.PropertyName == "y")  
            {
                this.y = toUpdateBall.y + valueToMove;
            }
        }

        
        public int XHandler
        {
            get { return x; }
            set
            {
                x = value;
                WhenPropertyChanged("x");
            }
        }


        public int YHandler
        {
            get { return y; }
            set
            {
                x = value;
                WhenPropertyChanged("y");
            }
        }


        public int RadiusHandler
        {
            get { return radius; } //no setter cause it's hardocded
        }

    }
}
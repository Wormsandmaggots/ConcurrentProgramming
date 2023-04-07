using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;

namespace Model
{
    public class BallModel
    {
        public int x;
        public int y;
        //Idk if we need here radius, if it's hardcoded.
        public int radius;

        public event PropertyChangedEventHandler PropretyChanged;

        protected void WhenPropertyChanged([CallerMemberName] string name = null)
        {
            this.PropretyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        //"Ball" is a placeholder for class about Ball in Logic
        public BallModel(Ball ball)
        {
            this.x = ball.x;
            this.y = ball.y;
            this.radius = ball.radius;
        }

        public void Update (object source, int valueToMove, PropertyChangedEventArgs eventCh)
        {
            Ball toUpdateBall = (Ball)source;

            if(eventCh.PropertyName == "x")
            {       //We need to talk over movement of the ball...
                this.x = toUpdateBall.x + valueToMove; 
            }
            if(eventCh.PropertyName == "y")  
            {
                this.y = toUpdateBall.y + valueToMove;
            }
        }

        

        #region x
        public int XHandler
        {
            get { return x; }
            set
            {
                x = value;
                WhenPropertyChanged("x");
            }
        }
        // public int getX(BallModel ballModel)
        // {
        //   return ballModel.x;
        //  }

        //public void setX(BallModel ballModel)
        //{
        /// <summary>
        ///  x = ballModel.x;
        /// </summary>
        // WhenPropertyChanged("x");
        //  }

        #endregion x

        #region y

        public int YHandler
        {
            get { return y; }
            set
            {
                x = value;
                WhenPropertyChanged("y");
            }
        }
       // public int getY(BallModel ballModel)
       // {
         //   return ballModel.y;
        //}

//        public void setY (BallModel ballModel)
  //      {
    //        y = ballModel.y;
      //      WhenPropertyChanged("y");
        //}

        #endregion y

        public int RadiusHandler
        {
            get { return radius; } //no setter cause it's hardocded
        }

    }
}
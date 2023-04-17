using System.Collections.ObjectModel;
using Logic;

namespace Model
{
    public abstract class AbstractModelApi
    {

        public static AbstractModelApi CreateApi()
        {   
            return new ModelApi();
        }

        public abstract IBallModel CreateBall(IBallLogic ballLogic);

        public abstract void MakeScene(int ballsCount, int radius);
        public abstract void Enable();

        public abstract void Disable();

        public abstract bool IsEnabled();

        public abstract ObservableCollection<IBallModel> GetAllBalls();

        public sealed class ModelApi : AbstractModelApi
        {
            private AbstractLogicApi logicApi = AbstractLogicApi.CreateApi();
            ObservableCollection<IBallModel> balls = new ObservableCollection<IBallModel>();

            public ObservableCollection<IBallModel> BallsListModel
            {
                get
                {
                    return balls;
                }
                set
                {
                    balls = value;
                }
            }

            public ModelApi()
            {
                    this.logicApi = AbstractLogicApi.CreateApi();
            }

            public override void Disable()
            {
                logicApi.Disable();
            }

            public override void Enable()
            {
                logicApi.Enable();
            }

            public override bool IsEnabled()
           {
                return logicApi.IsEnabled();
            }

            public override void MakeScene(int ballsCount, int radius)
            {
                logicApi.CreateScene(520, 500, ballsCount, radius);
            }

            public override ObservableCollection<IBallModel> GetAllBalls()
            {
                List<IBallLogic> ballslist = logicApi.GetBalls();
                BallsListModel.Clear();
                foreach (IBallLogic ball in ballslist)
                {
                    BallsListModel.Add(new BallModel(ball));
                }
                return BallsListModel;
            }

            public override IBallModel CreateBall(IBallLogic ballLogic)
            {
                //if(ballLogic == null)
               // {
                   
               //     return new BallModel(AbstractLogicApi.CreateApi().CreateBall());
               // }
                return new BallModel(ballLogic);

            }
        }
    }
}

using System.Collections.ObjectModel;
using Logic;

namespace Model
{
    public abstract class AbstractModelApi
    {
        public static AbstractModelApi CreateApi(AbstractLogicApi logicApi = null)
        {
            return new ModelApi();
        }

        public abstract void MakeScene(int ballsCount, int radius);
        public abstract void Enable();

        public abstract void Disable();

        public abstract bool IsEnabled();

        public abstract ObservableCollection<BallModel> GetAllBalls();

        public sealed class ModelApi : AbstractModelApi
        {
            private AbstractLogicApi logicApi = AbstractLogicApi.CreateApi();
            ObservableCollection<BallModel> balls = new ObservableCollection<BallModel>();

            public ObservableCollection<BallModel> BallsListModel
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

            public ModelApi(AbstractLogicApi logicApi = null)
            {
                if (logicApi == null)
                {
                    this.logicApi = AbstractLogicApi.CreateApi();
                }
                else
                {
                    this.logicApi = logicApi;
                }
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

            public override ObservableCollection<BallModel> GetAllBalls()
            {
                List<BallLogic> ballslist = logicApi.GetBalls();
                BallsListModel.Clear();
                foreach (BallLogic ball in ballslist)
                {
                    BallsListModel.Add(new BallModel(ball));
                }
                return BallsListModel;
            }
        }
    }
}

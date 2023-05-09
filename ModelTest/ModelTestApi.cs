using Logic;
using Model;
using System.Collections.ObjectModel;
using LogicTests;

namespace ModelTest
{
    public abstract class CreateModelApiTest
    {
        public static AbstractModelApi GetModelTestApi()
        {
            return new ModelTestApi();
        }
    }

    internal class ModelTestApi : AbstractModelApi
    {
        private AbstractLogicApi logicApi = CreateLogicTestApi.GetLogicTestApi();
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

        public ModelTestApi()
        {
            this.logicApi = CreateLogicTestApi.GetLogicTestApi();
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

            foreach (IBallModel ballModel in BallsListModel)
            {
                ballModel.Dispose();
            }

            BallsListModel.Clear();
            foreach (IBallLogic ball in ballslist)
            {
                BallsListModel.Add(new BallTestModel(ball));
            }
            return BallsListModel;
        }

        public override IBallModel CreateBall(IBallLogic ballLogic)
        {
            //if(ballLogic == null)
            // {

            //     return new BallModel(AbstractLogicApi.CreateApi().CreateBall());
            // }
            return new BallTestModel(ballLogic);

        }
    }
}

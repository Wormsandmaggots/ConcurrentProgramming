using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public abstract class AbstractModelApi
    {
        //LogicApi placeholder for LogicApi
        //public static AbstractModelApi CreateApi(LogicApi logicApi = null)
        //{
        //    return new ModelApi();
        //}

        public abstract void MakeScene();
        public abstract void Enable();

        public abstract void Disable();

        public abstract bool IsEnabled();

        //public sealed class ModelApi : AbstractModelApi
        //{   //same here waiting for AbstractLogicApi
        //    //private AbstractLogicApi logicApi = AbstractLogicApi.CreateApi(null);
        //    ObservableCollection<BallModel> balls = new ObservableCollection<BallModel>();

        //    public ObservableCollection<BallModel> BallsListModel
        //    {
        //        get
        //        {
        //            return balls;
        //        }
        //        set
        //        {
        //            balls = value;
        //        }
        //    }

        //    public ModelApi(AbstractLogicApi logicApi = null)
        //    {
        //        if (logicApi == null)
        //        {
        //            this.logicApi = AbstractLogicApi.CreateApi();
        //        }
        //        else
        //        {
        //            this.logicApi = logicApi;
        //        }
        //    }

        //    public override void Disable()
        //    {
        //        logicApi.Disable();
        //    }

        //    public override void Enable()
        //    {
        //        logicApi.Enable();
        //    }

        //    public override bool IsEnabled()
        //    {
        //        return logicApi.isEnabled();
        //    }

        //    //Need parameters, waiting for logicApi.MakeScene() method
        //    public override void MakeScene()
        //    {
        //        logicApi.MakeScene();
        //    }
        //}
    }
}

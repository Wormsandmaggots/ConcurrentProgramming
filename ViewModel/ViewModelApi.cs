using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ViewModel
{
    public class ViewModelApi : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private int ballCounter = 1;
        AbstractModelApi modelApi;

        public AbstractModelApi GetModelApi()
        {
            return modelApi;
        }
        public string BallCounterHandler
        {
            get
            {
                return Convert.ToString(ballCounter);
            }
            set
            {
                if (value.Length == 0) return;

                ballCounter = Convert.ToInt32(value);
                OnPropertyChanged("BallCounterHandler");
            }
        }

        private int radius = 15; //here is hardcoded, idk if it should be here

        public ICommand EnableSignal
        {
            get;
            set;
        }
        public ICommand DisableSignal
        {
            get;
            set;
        }

        private ObservableCollection<BallModel> ballList;

        public ObservableCollection<BallModel> BallsListHandler
        {
            get
            {
                return ballList;
            }

            set
            {
                if (value.Equals(this.ballList)) return;
                ballList = value;
                OnPropertyChanged("BallsListHandler");
            }
        }

        public bool isEnabled = true;

        public bool IsEnabledHandler
        {
            get { return isEnabled; }
            set
            {
                isEnabled = value;
                OnPropertyChanged("IsEnabledHandler");
            }
        }

      

        public void enable()
        {
            modelApi.MakeScene(ballCounter, radius);
            modelApi.Enable();
            isEnabled = true;
            BallsListHandler = modelApi.GetAllBalls();
        }

        public void disable()
        {
            modelApi.Disable();
            isEnabled = false;
        }

        public ViewModelApi() : this(null) { }

        public ViewModelApi(AbstractModelApi api = null)
        {
            EnableSignal = new Signal(enable);
            DisableSignal = new Signal(disable);

            if(api == null)
            {
                this.modelApi = AbstractModelApi.CreateApi();
            }
            else
            {
                this.modelApi = api;
            }
        }

        ~ ViewModelApi()
        {
            PropertyChanged = null;
        }


    }
}

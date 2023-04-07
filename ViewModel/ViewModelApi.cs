using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
    internal class ViewModelApi : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void WhenPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private int ballCounter = 1;
        AbstractModelApi modelApi;

        public string BallCounterHandler
        {
            get
            {
                return Convert.ToString(ballCounter);
            }
            set
            {
                ballCounter = Convert.ToInt32(value);
                WhenPropertyChanged("BallCounter");
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
                WhenPropertyChanged("BallsListHandler");
            }
        }

        public bool isEnabled = true;

        public bool IsEnabledHandler
        {
            get { return isEnabled; }
            set
            {
                isEnabled = value;
                WhenPropertyChanged("IsEnabled");
            }
        }

        private void enable()
        {
            modelApi.MakeScene(ballCounter, radius);
            modelApi.Enable();
            isEnabled = true;
            BallsListHandler = modelApi.GetAllBalls();
        }

        private void disable()
        {
            modelApi.Disable();
            isEnabled = false;
        }

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

        
    }
}

using System.Windows.Input;

namespace ViewModel
{
    internal class Signal :ICommand
    {
        private readonly Action execute;
        private readonly Func<bool> canExecute;

        public event EventHandler CanExecuteChanged;

        public Signal(Action execute, Func<bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public void Execute(object name)
        {
            this.execute(); 
        }

        public bool CanExecute(object name)
        {
            if(this.canExecute == null)
            {
                return true;
            }
            else
            {
                return this.canExecute();
            }
        }

        internal void RiseCanExecuteChanged() 
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);    
        }
    }
}
using System.Windows.Input;

namespace ViewModel
{
    internal class Signal :ICommand
    {
        private Action execute;
        private Func<bool> canExecute;

        public event EventHandler? CanExecuteChanged;

        public Signal(Action execute, Func<bool> canExecute = null)
        {
            this.execute += execute;
            this.canExecute += canExecute;
        }

        ~Signal()
        {
            execute = (Action)Delegate.RemoveAll(execute, execute);
            canExecute = (Func<bool>)Delegate.RemoveAll(canExecute, canExecute);
            CanExecuteChanged = null;
        }

        public void Execute(object name)
        {
            execute?.Invoke();
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
using System.Windows.Input;

namespace IdealTimeTracker.WPF.Command
{
    public abstract class AsyncCommandBase : ICommand
    {
        public bool isExecuting;
        public bool IsExecuting
        {
            get =>  isExecuting;
            set { 
                isExecuting = value;
                OnCanExecuteChanged();  
            }
        }   
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return !IsExecuting;
        }

        public async void Execute(object? parameter)
        {
            try
            {
                isExecuting = true;
                await ExecuteAsync(parameter);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                isExecuting = false;
            }
        }

        protected abstract  Task ExecuteAsync(object? paramter);

        private void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}

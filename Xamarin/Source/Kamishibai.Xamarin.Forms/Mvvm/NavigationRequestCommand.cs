using System;
using System.Windows.Input;

namespace Kamishibai.Xamarin.Forms.Mvvm
{
    public class NavigationRequestCommand<TParam> : NavigationRequest<TParam>, ICommand
    {
        private readonly Action<TParam> _execute;
        private readonly Func<TParam, bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public NavigationRequestCommand() : this(_ => { }, _ => true)
        {
        }

        public NavigationRequestCommand(Action execute)
        {
            if(execute == null) throw new ArgumentNullException(nameof(execute));

            _execute = _ => execute();
            _canExecute = _ => true;
        }

        public NavigationRequestCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null) throw new ArgumentNullException(nameof(execute));
            if (canExecute == null) throw new ArgumentNullException(nameof(canExecute));

            _execute = _ => execute();
            _canExecute = _ => canExecute();
        }

        public NavigationRequestCommand(Action<TParam> execute) : this(execute, _ => true)
        {
        }

        public NavigationRequestCommand(Action<TParam> execute, Func<TParam, bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }

        public bool CanExecute(TParam parameter = default(TParam))
        {
            return _canExecute(parameter);
        }

        public bool CanExecute(object parameter)
        {
            if (parameter is TParam)
            {
                return CanExecute((TParam) parameter);
            }
            else
            {
                return CanExecute();
            }
        }

        public void Execute(TParam parameter = default(TParam))
        {
            _execute(parameter);
            RaiseAsync(parameter);
        }

        public void Execute(object parameter)
        {
            if (parameter is TParam)
            {
                Execute((TParam)parameter);
            }
            else
            {
                Execute();
            }
        }

        public void ChangeCanExecute()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    
    public class NavigationRequestCommand : NavigationRequest, ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public NavigationRequestCommand() : this(() => { }, () => true)
        {
        }

        public NavigationRequestCommand(Action execute) : this(execute, () => true)
        {
        }
        public NavigationRequestCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute();
            RaiseAsync();
        }

        public void ChangeCanExecute()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}

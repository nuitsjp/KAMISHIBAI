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

        public NavigationRequestCommand(Action execute) : this(_ => execute(), _ => true)
        {
        }
	    
        public NavigationRequestCommand(Action execute, Func<bool> canExecute) : this(_ => execute?.Invoke(), _ => canExecute?.Invoke() ?? true)
        {
        }

        public NavigationRequestCommand(Action<TParam> execute) : this(execute, _ => true)
        {
        }
        public NavigationRequestCommand(Action<TParam> execute, Func<TParam, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(TParam parameter = default(TParam))
        {
            return _canExecute?.Invoke(parameter) ?? true;
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
            _execute?.Invoke(parameter);
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
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke() ?? true;
        }

        public void Execute(object parameter)
        {
            _execute?.Invoke();
            RaiseAsync();
        }

        public void ChangeCanExecute()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}

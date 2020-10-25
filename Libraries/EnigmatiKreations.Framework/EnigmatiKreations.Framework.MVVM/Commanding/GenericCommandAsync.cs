using log4net.Core;
using EnigmatiKreations.Framework.Utils.Extensions;
using Randomizer.Framework.ViewModels.Commanding.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Randomizer.Framework.Models.Contract;

namespace Randomizer.Framework.ViewModels.Commanding
{

    public interface IGenericCommandAsync<T> : ICommand
    {
        Task ExecuteAsync(T param);
        bool CanExecute();
    }

    public class GenericCommandAsync<T> : IGenericCommandAsync<T>, IReportProgressCommand
    {
        public event EventHandler CanExecuteChanged;

        private bool _isExecuting;
        private readonly Action<T> _execute;
        private readonly Func<bool> _canExecute;
        private readonly IErrorHandler _errorHandler;

        public GenericCommandAsync(Action<T> execute, Func<bool> canExecute = null, IErrorHandler errorHandler = null)
        {
            _execute = execute;
            _canExecute = canExecute;
            _errorHandler = errorHandler;
        }

        public bool CanExecute()
        {
            return !_isExecuting && (_canExecute?.Invoke() ?? true);
        }

        public bool CanExecute(object parameter)
        {
            return !_isExecuting && (_canExecute?.Invoke() ?? true);
        }

        public void Execute(T parameter)
        {
            ExecuteAsync(parameter).FireAndForgetSafeAsync(_errorHandler);
        }

        public async Task ExecuteAsync(T param)
        {
            if (CanExecute(param))
            {
                try
                {
                    _isExecuting = true;
                    await Task.Run(() => { _execute(param); });
                }
                finally
                {
                    _isExecuting = false;
                }
            }

            RaiseCanExecuteChanged();
        }

        public void ReportProgress(Action action)
        {
            Task.Run(action);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #region Explicit Implementations

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute();
        }

        void ICommand.Execute(object parameter)
        {
            ExecuteAsync((T)parameter).FireAndForgetSafeAsync(_errorHandler);
        }
        #endregion
    }
}

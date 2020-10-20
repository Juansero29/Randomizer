﻿using log4net.Core;
using EnigmatiKreations.Framework.Utils.Extensions;
using Randomizer.Framework.ViewModels.Commanding.Contract;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using EnigmatiKreations.Framework.MVVM.BaseViewModels;

namespace Randomizer.Framework.ViewModels.Commanding
{

    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync();
        bool CanExecute();
    }



    public class SimpleCommandAsync : BaseViewModel, IAsyncCommand, IReportProgressCommand, INotifyPropertyChanged
    {
        public event EventHandler CanExecuteChanged;

        private readonly Action _execute;
        private readonly Func<bool> _canExecute;
        private readonly IErrorHandler _errorHandler;



        private bool _IsExecuting;

        /// <summary>
        /// Is this command executing
        /// </summary>
        public bool IsExecuting
        {
            get => _IsExecuting;
            set => SetValue(ref _IsExecuting, value);
        }

        public SimpleCommandAsync(Action execute, Func<bool> canExecute = null, IErrorHandler errorHandler = null)
        {
            _execute = execute;
            _canExecute = canExecute;
            _errorHandler = errorHandler;
        }

        public bool CanExecute()
        {
            return !_IsExecuting && (_canExecute?.Invoke() ?? true);
        }

        public async Task ExecuteAsync()
        {
            if (CanExecute())
            {
                try
                {
                    if(_execute.Target is BasePageViewModel vm)
                    {
                        vm.IsBusy = true;
                    }

                    IsExecuting = true;
                    await Task.Run(() => _execute());

                    if (_execute.Target is BasePageViewModel vm2)
                    {
                        vm2.IsBusy = false;
                    }
                }
                finally
                {
                    IsExecuting = false;
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

        #region Explicit implementations
        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute();
        }

        void ICommand.Execute(object parameter)
        {
            ExecuteAsync().FireAndForgetSafeAsync(_errorHandler);
        }
        #endregion
    }
}
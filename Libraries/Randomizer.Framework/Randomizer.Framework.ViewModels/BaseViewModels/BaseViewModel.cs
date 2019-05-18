using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

namespace Randomizer.Framework.ViewModels
{
    /// <summary>
    /// Class serving as the base for any ViewModel
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {

        #region Fields
        bool _IsBusy = false;
        string _Title = string.Empty;
        #endregion

        /// <summary>
        /// Property indicating if the ViewModel is busy loading
        /// something to show a visual feedback to the user.
        /// </summary>
        public bool IsBusy
        {
            get => _IsBusy;
            set => SetProperty(ref _IsBusy, value);
        }

        /// <summary>
        /// The title of this ViewModel 
        /// </summary>
        public string Title
        {
            get => _Title;
            set => SetProperty(ref _Title, value);
        }

        /// <summary>
        /// Sets the backing store field to the value and raises <see cref="OnPropertyChanged(string)"/>
        /// if the field has a different value than the property corresponding to 'propertyName'
        /// </summary>
        /// <typeparam name="T">The type of the property to set</typeparam>
        /// <param name="backingStore">The field that contains the value</param>
        /// <param name="value">The new value to set</param>
        /// <param name="propertyName">The name of the property to be set (by default we use the <see cref="CallerMemberNameAttribute"/></param>
        /// <param name="onChanged">An action to invoke whenever the property changes</param>
        /// <returns></returns>
        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}

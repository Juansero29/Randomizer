using EnigmatiKreations.Framework.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ControlsGallery
{
    public partial class MainPage : ContentPage
    {


        public string CurrentControlType { get; set; }

        private readonly ResourceDictionary _ThemeDictionary;
        private bool _isDarkThemeSwitchedOn;

        public MainPage()
        {
            InitializeComponent();
            ThemeSwitch.Toggled += Switch_Toggled;
            ControlsCarouselView.ItemAppearing += ControlsCarouselView_ItemAppearing; ;
            _ThemeDictionary = GetThemeDictionaryFromApp();
        }

        private void ControlsCarouselView_ItemAppearing(PanCardView.CardsView view, PanCardView.EventArgs.ItemAppearingEventArgs args)
        {
            CurrentControlType = view.CurrentView.GetType().ToString();
            OnPropertyChanged(nameof(CurrentControlType));
        }

        private ResourceDictionary GetThemeDictionaryFromApp()
        {
            if (Application.Current is not App app) throw new InvalidCastException();
            return app.ThemeResourceDictionary;
        }

        #region Switch Theme
        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            _isDarkThemeSwitchedOn = e.Value;
            SetDarkThemeOrLightTheme();
        }

        private void SetDarkThemeOrLightTheme()
        {
            if (_ThemeDictionary == null) return;
            ClearCurrentTheme();
            if (_isDarkThemeSwitchedOn)
            {
                SetDarkTheme();
            }
            else
            {
                SetLightTheme();
            }
        }

        private void ClearCurrentTheme()
        {
            _ThemeDictionary.MergedDictionaries.Clear();
        }

        private void SetDarkTheme()
        {
            Application.Current.UserAppTheme = OSAppTheme.Dark;
            _ThemeDictionary.MergedDictionaries.Add(new DarkTheme());
        }

        private void SetLightTheme()
        {
            Application.Current.UserAppTheme = OSAppTheme.Light;
            _ThemeDictionary.MergedDictionaries.Add(new LightTheme());
        }

        #endregion


    }
}

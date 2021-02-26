using EnigmatiKreations.Framework.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ControlsGallery
{
    public partial class MainPage : ContentPage
    {

        public string CurrentControlType { get; set; }

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            var themeMergedDictionary = Application.Current.Resources.MergedDictionaries.Where(d => d.MergedDictionaries.Count == 1).FirstOrDefault();
            if (themeMergedDictionary != null) themeMergedDictionary.MergedDictionaries.Clear();
            if (e.Value)
            {
                Application.Current.UserAppTheme = OSAppTheme.Light;
                themeMergedDictionary.MergedDictionaries.Add(new LightTheme());
            }
            else
            {
                Application.Current.UserAppTheme = OSAppTheme.Dark;
                themeMergedDictionary.MergedDictionaries.Add(new DarkTheme());
            }
        }

        private void CarouselView_ItemSwiped(PanCardView.CardsView view, PanCardView.EventArgs.ItemSwipedEventArgs args)
        {
            CurrentControlType = view.CurrentView.GetType().ToString();
            OnPropertyChanged(nameof(CurrentControlType));
        }
    }
}

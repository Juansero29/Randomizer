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
        public ICommand LongPressCommand { get; set; }
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            LongPressCommand = new Command(LongPress);
        }

        private void LongPress(object obj)
        {
            throw new NotImplementedException();
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



        private void CarouselView_ItemAppeared(PanCardView.CardsView view, PanCardView.EventArgs.ItemAppearedEventArgs _)
        {
            CurrentControlType = view.CurrentView.GetType().ToString();
            OnPropertyChanged(nameof(CurrentControlType));
        }
    }
}

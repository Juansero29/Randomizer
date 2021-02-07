using EnigmatiKreations.Framework.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;

namespace Randomizer.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        private Ellipse _Circle;
        private readonly uint _AnimationSpeed = 900;

        public SettingsPage()
        {
            InitializeComponent();
        }

        private void CreateCircle()
        {
            _Circle = new Ellipse
            {
                AnchorX = 1,
                AnchorY = 1,
                HeightRequest = 100,
                WidthRequest = 100,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Scale = 1
            };
            Root.Children.Insert(0, _Circle);
            AbsoluteLayout.SetLayoutBounds(_Circle, new Xamarin.Forms.Rectangle(3, 2, 100, 100));
            AbsoluteLayout.SetLayoutFlags(_Circle, AbsoluteLayoutFlags.PositionProportional);
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            CreateCircle();
            await ScaleUpCircle();
            SetDefaultValueOnCheckBox();
        }

        private void SetDefaultValueOnCheckBox()
        {
            Checkbox.IsChecked = Application.Current.UserAppTheme == OSAppTheme.Light;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Root.Children.RemoveAt(0);
        }


        private async Task ScaleDownCircle()
        {
            Checkbox.IsEnabled = false;
            await _Circle.ScaleTo(1, length: _AnimationSpeed / 2);
        }

        private async Task ScaleUpCircle()
        {
            var brush = Application.Current.UserAppTheme == OSAppTheme.Dark ? Brush.White : Brush.Black;
            _Circle.Fill = brush;
            await _Circle.ScaleTo(11, length: _AnimationSpeed, easing: Easing.SinInOut);
            Checkbox.IsEnabled = true;
        }

        private async void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var themeMergedDictionary = Application.Current.Resources.MergedDictionaries.Where(d => d.MergedDictionaries.Count == 1).FirstOrDefault();
            if (themeMergedDictionary != null) themeMergedDictionary.MergedDictionaries.Clear();
            await ScaleDownCircle();
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
            await ScaleUpCircle();

        }

    }
}
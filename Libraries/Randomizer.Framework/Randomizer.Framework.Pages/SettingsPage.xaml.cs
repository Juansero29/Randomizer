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

            var themeMergedDictionary = Application.Current.Resources.MergedDictionaries.Where(d => d.MergedDictionaries.Count == 1).FirstOrDefault().MergedDictionaries.First();
            var brush = default(Brush);
            if (themeMergedDictionary is DarkTheme)
            {
                brush = Brush.White;
            }
            else
            {
                brush = Brush.Black;
            }
            _Circle.Fill = brush;
            _Circle.IsVisible = true;
            await _Circle.ScaleTo(11, length: _AnimationSpeed, easing: Easing.SinInOut);
            Checkbox.IsEnabled = true;
        }

        private async void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var themeMergedDictionary = Application.Current.Resources.MergedDictionaries.Where(d => d.MergedDictionaries.Count == 1).FirstOrDefault();
            if (themeMergedDictionary != null) themeMergedDictionary.MergedDictionaries.Clear();
            if (e.Value)
            {
                await ScaleDownCircle();
                Application.Current.UserAppTheme = OSAppTheme.Light;
                themeMergedDictionary.MergedDictionaries.Add(new LightTheme());
                await ScaleUpCircle();
            }
            else
            {
                await ScaleDownCircle();
                Application.Current.UserAppTheme = OSAppTheme.Dark;
                themeMergedDictionary.MergedDictionaries.Add(new DarkTheme());
                await ScaleUpCircle();

            }
        }

    }
}
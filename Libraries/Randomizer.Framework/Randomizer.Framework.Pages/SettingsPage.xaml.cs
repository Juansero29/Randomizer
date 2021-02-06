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

        Ellipse Circle;
        public SettingsPage()
        {
            InitializeComponent();

        }

        private void CreateCircle()
        {
            Circle = new Ellipse
            {
                AnchorX = 1,
                AnchorY = 1,
                HeightRequest = 100,
                WidthRequest = 100,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Scale = 1
            };
            Root.Children.Insert(0, Circle);
            AbsoluteLayout.SetLayoutBounds(Circle, new Xamarin.Forms.Rectangle(3, 2, 100, 100));
            AbsoluteLayout.SetLayoutFlags(Circle, AbsoluteLayoutFlags.PositionProportional);
        }

        private uint _animationSpeed = 900;

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            CreateCircle();

            await Circle.ScaleTo(1, length: 1);
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

            Circle.Fill = brush;
            Circle.IsVisible = true;
            await Circle.ScaleTo(11, length: _animationSpeed, easing: Easing.SinInOut);
        }


        private async void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            Checkbox.IsEnabled = false;
            var originalBounds = Circle.Bounds;
            var originalScale = Circle.Scale;
            var brush = default(Brush);


            await Circle.ScaleTo(1, length: _animationSpeed, easing: Easing.SinInOut);
            var themeMergedDictionary = Application.Current.Resources.MergedDictionaries.Where(d => d.MergedDictionaries.Count == 1).FirstOrDefault();
            if (themeMergedDictionary != null) themeMergedDictionary.MergedDictionaries.Clear();

            if (e.Value)
            {
                Application.Current.UserAppTheme = OSAppTheme.Light;
                themeMergedDictionary.MergedDictionaries.Add(new LightTheme());
                brush = Brush.Black;

            }
            else
            {
                Application.Current.UserAppTheme = OSAppTheme.Dark;
                themeMergedDictionary.MergedDictionaries.Add(new DarkTheme());
                brush = Brush.White;
            }
            Circle.Fill = brush;
            await Circle.ScaleTo(11, length: _animationSpeed, easing: Easing.SinInOut);
            Checkbox.IsEnabled = true;
        }



    }
}
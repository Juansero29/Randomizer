using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Randomizer.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private uint _animationSpeed = 900;

        private async void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            Checkbox.IsEnabled = false;
            var originalBounds = Circle.Bounds;
            var originalScale = Circle.Scale;
            var color = default(Color);
            var brush = default(Brush);
            if (e.Value)
            {
                Application.Current.UserAppTheme = OSAppTheme.Dark;

                color = (Color)Application.Current.Resources["DarkPrimaryColor"];
                brush = Brush.Black;
            }
            else
            {
                Application.Current.UserAppTheme = OSAppTheme.Light;

                color = (Color)Application.Current.Resources["LightPrimaryColor"];
                brush = Brush.White;
            }

            Circle.IsVisible = true;

            var rect = new Rectangle(0, 0, 1000, 1000);
            Circle.Fill = brush;
            await Circle.LayoutTo(rect, length: _animationSpeed, easing: Easing.SinInOut);
            Circle.IsVisible = true;
            Checkbox.IsEnabled = true;
            InvalidateMeasure();
        }
    }
}
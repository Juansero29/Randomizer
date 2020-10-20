using EnigmatiKreations.Framework.Services.i18n;
using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EnigmatiKreations.Framework.Services.Resources;

namespace Randomizer
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            SetCurrentLanguage();
            MainPage = new AppShell();
        }

        private void SetCurrentLanguage()
        {
            var ci = DependencyService.Get<ILocalizationService>().GetCurrentCultureInfo();
            TextResources.Culture = ci;
            DependencyService.Get<ILocalizationService>().SetLocale(ci); // set the Thread for locale-aware methods
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

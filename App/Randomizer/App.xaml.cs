using EnigmatiKreations.Framework.MVVM.BaseViewModels;
using EnigmatiKreations.Framework.Services.Alerts;
using EnigmatiKreations.Framework.Services.Navigation;
using Randomizer.Framework.Persistence;
using Randomizer.Framework.Services.Alerts;
using Randomizer.Framework.Services.i18n;
using Randomizer.Framework.Services.Navigation;
using Randomizer.Framework.Services.Resources;
using Xamarin.Forms;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Randomizer.Framework.ViewModels.Business;
using System;
using Randomizer.Pages;

namespace Randomizer
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            SetCurrentLanguage();
            RegisterServicesInContainer();
            MainPage = new AppShell();
        }

        private void RegisterServicesInContainer()
        {
            Container.PrepareNewBuilder();
            Container.RegisterDependency(new ShellNavigationService(), typeof(INavigationService), true);
            Container.RegisterDependency(new AlertsService(), typeof(IAlertsService), true);
            Container.RegisterDependency(new ListsManagerVM(new ListsManager()), typeof(ListsManager), true);
            Container.BuildContainer();
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
            AppCenter.Start("android=fe6df48a-5cbf-4ab5-aa33-f92cecff4e06;" +
                  "ios=26d40430-7a80-455f-a5ce-241972cfe9ba;",
                  typeof(Analytics), typeof(Crashes));
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

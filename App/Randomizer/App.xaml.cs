using EnigmatiKreations.Framework.Managers.Contract;
using EnigmatiKreations.Framework.MVVM.BaseViewModels;
using EnigmatiKreations.Framework.Services.Alerts;
using EnigmatiKreations.Framework.Services.Navigation;
using Randomizer.Framework.Models.Contract;
using Randomizer.Framework.Persistence;
using Randomizer.Framework.Services.Alerts;
using Randomizer.Framework.Services.i18n;
using Randomizer.Framework.Services.Navigation;
using Randomizer.Framework.Services.Resources;
using Xamarin.Forms;


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
            Container.RegisterDependency(new ListsManager(), typeof(ListsManager), true);
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

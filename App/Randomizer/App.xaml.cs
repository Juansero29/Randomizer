﻿using EnigmatiKreations.Framework.MVVM.BaseViewModels;
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
using System.Globalization;
using Randomizer.Framework.Pages.Navigation;
using System.Linq;
using EnigmatiKreations.Framework.Controls;
using EnigmatiKreations.Framework.Utils;
using System.Reflection;
using System.Diagnostics;
using Xamarin.Forms.Svg;

namespace Randomizer
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            SvgImageSource.RegisterAssembly();
            SetCurrentLanguage();
            SetCurrentTheme();
            PrintEmbeddedResources();


            MainPage = new AppShellPage();
            RegisterServicesInContainer();

            (Container.Resolve<INavigationService>().GetCurrentPage().BindingContext as BasePageViewModel).LoadCommand.Execute(null);
        }

        private void SetCurrentTheme()
        {
            if(Current.UserAppTheme == OSAppTheme.Unspecified)
            {
                Current.UserAppTheme = OSAppTheme.Dark;
            }

            var themeMergedDictionary = Current.Resources.MergedDictionaries.Where(d => d.MergedDictionaries.Count == 1).FirstOrDefault();
            if (themeMergedDictionary != null) themeMergedDictionary.MergedDictionaries.Clear();
            if (Current.UserAppTheme == OSAppTheme.Light)
            {
                
                themeMergedDictionary.MergedDictionaries.Add(new LightTheme());
            }
            else
            {
                themeMergedDictionary.MergedDictionaries.Add(new DarkTheme());
            }
        }

        private void RegisterServicesInContainer()
        {
            do
            {
                Container.PrepareNewBuilder();
                var navService = new ShellNavigationService();
                navService.Initialize(new NavigationPage(new Page()), new RandomizerPageLoader());
                Container.RegisterDependency(navService, typeof(INavigationService), true);
                Container.RegisterDependency(new AlertsService(), typeof(IAlertsService), true);
                Container.RegisterDependency(new ListsManagerVM(new ListsManager()), typeof(ListsManagerVM), true);
            } while (!Container.BuildContainer());
        }

        private void SetCurrentLanguage()
        {
            try
            {
                var localizationService = DependencyService.Get<ILocalizationService>();
                var ci = localizationService.GetCurrentCultureInfo();
                TextResources.Culture = ci;
                DependencyService.Get<ILocalizationService>().SetLocale(ci); // set the Thread for locale-aware methods
            }
            catch (Exception)
            {
                Console.WriteLine("Couldn't find the locale. Setting a default one.");
                TextResources.Culture = new CultureInfo("en-US");
            }

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

        public static void PrintEmbeddedResources()
        {
#if DEBUG
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var embeddedResources = "Embedded Resources:" + Environment.NewLine;
            foreach (var resourceName in assembly.GetManifestResourceNames())
            {
                Debug.WriteLine("Found Resource: " + resourceName);
            }
#endif
        }
    }
}

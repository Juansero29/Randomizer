﻿using Randomizer.Framework.Services.i18n;
using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            Framework.Services.Resources.TextResources.Culture = ci;
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

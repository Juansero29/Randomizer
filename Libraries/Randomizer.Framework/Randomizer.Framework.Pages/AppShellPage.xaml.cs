using EnigmatiKreations.Framework.Services.Navigation;
using Randomizer.Framework.Services.Navigation;
using Randomizer.Pages;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Randomizer.Pages
{
    public partial class AppShellPage : Xamarin.Forms.Shell
    {
        private static Dictionary<string, Type> Routes = new Dictionary<string, Type>()
        {
            { NavigationRoutes.ListEditionPage, typeof(ListEditionPage) },
            { NavigationRoutes.HomePage, typeof(HomePage) }
        };

        public AppShellPage()
        {
            InitializeComponent();
            RegisterRoutes();
        }

        // Register additional routes which aren't represented in the Shell.xaml file
        private void RegisterRoutes()
        {
            foreach (var route in Routes)
            {
                Routing.RegisterRoute(route.Key, route.Value);
            }
        }
    }
}

using Randomizer.Framework.Services.Navigation;
using Randomizer.Pages;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Randomizer
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        private static Dictionary<string, Type> Routes = new Dictionary<string, Type>()
        {
            { "listedition", typeof(ListEditionPage) }
        };

        public AppShell()
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

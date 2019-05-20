using Randomizer.Views;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Randomizer
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
        }

        // Register additional routes which aren't represented in the Shell.xaml file
        private void RegisterRoutes()
        {
            Routing.RegisterRoute("listedition", typeof(ListEditionPage));
        }
    }
}

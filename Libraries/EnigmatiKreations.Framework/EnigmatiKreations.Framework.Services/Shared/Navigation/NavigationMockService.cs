﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnigmatiKreations.Framework.Services.Navigation;
using Xamarin.Forms;

namespace EnigmatiKreations.Framework.Services.Navigation
{
    public class NavigationMockService : INavigationService
    {
        public Page GetCurrentPage()
        {
            return Application.Current.MainPage;
        }

        public Task GoToAsync(string uri)
        {
            return Task.CompletedTask;
        }

        public Task PopAsync()
        {
            return Task.CompletedTask;
        }
    }
}
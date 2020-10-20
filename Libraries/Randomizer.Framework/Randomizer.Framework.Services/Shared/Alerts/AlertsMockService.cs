using EnigmatiKreations.Framework.Services.Alerts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Randomizer.Framework.Services.Alerts
{
    public class AlertsMockService : IAlertsService
    {
        public Task DisplayAlert(string title, string message, string cancelMessage = null)
        {
            return Task.CompletedTask;
        }

        public Task<bool> DisplayAlertForResult(string title, string message, string acceptMessage, string refuseMessage)
        {
            return Task.FromResult(true);
        }

        public Task ShowFeatureNotImplementedAlert(string messageToShow = null)
        {
            return Task.CompletedTask;
        }
    }
}

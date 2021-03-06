﻿using EnigmatiKreations.Framework.Services.Alerts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Randomizer.Framework.Services.Alerts
{
    /// <summary>
    /// A simple service that shows alerts to the user
    /// </summary>
    public class AlertsService : IAlertsService
    {
        /// <remarks>
        /// Consider using a localized string by using the RESX file associated to <see cref="Resources.TextResources"/> 
        /// By default, it uses the text resource <see cref="Resources.TextResources.FeatureNotImplementedMessage"/> which is translated to all supported languages.
        /// </remarks>
        public Task ShowFeatureNotImplementedAlert(string messageToShow = null)
        {
            // If not message is given, use the text resource in the library
            messageToShow = string.IsNullOrWhiteSpace(messageToShow) ? Resources.TextResources.FeatureNotImplementedMessage : messageToShow;
            return DisplayAlert(Resources.TextResources.OopsMessage, messageToShow);
        }

        public Task DisplayAlert(string title, string message, string cancelMessage = null)
        {
            cancelMessage = string.IsNullOrWhiteSpace(cancelMessage) ? Resources.TextResources.OKMessage : cancelMessage;
            return Application.Current.MainPage.DisplayAlert(title, message, cancelMessage);
        }

        public Task<bool> DisplayAlertForResult(string title, string message, string acceptMessage, string refuseMessage)
        {
            return Application.Current.MainPage.DisplayAlert(title, message, acceptMessage, refuseMessage);
        }
    }
}

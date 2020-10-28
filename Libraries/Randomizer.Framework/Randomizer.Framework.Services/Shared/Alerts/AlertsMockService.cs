using EnigmatiKreations.Framework.Services.Alerts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Randomizer.Framework.Services.Alerts
{
    public class AlertsMockService : IAlertsService
    {
        public Task DisplayAlert(string title, string message, string cancelMessage = null)
        {

            return Task.Run(() =>
            {
                Console.WriteLine("• ALERT •");
                Console.WriteLine($"  TITLE: {title}");
                Console.WriteLine($"  MESSAGE: {message}");
                Console.WriteLine($"  CANCEL MESSAGE: {cancelMessage}");
            });
        }

        public Task<bool> DisplayAlertForResult(string title, string message, string acceptMessage, string refuseMessage)
        {
            Task.Run(() =>
            {
                Console.WriteLine("• ALERT •");
                Console.WriteLine($"  TITLE: {title}");
                Console.WriteLine($"  MESSAGE: {message}");
                Console.WriteLine($"  ACCEPT MESSAGE: {acceptMessage}");
                Console.WriteLine($"  REFUSE MESSAGE: {refuseMessage}");
            });
            return Task.FromResult(true);
        }

        public Task ShowFeatureNotImplementedAlert(string messageToShow = null)
        {
            return Task.Run(() =>
            {
                Console.WriteLine("• ShowFeatureNotImplementedAlert •");
                Console.WriteLine($"  TITLE: {messageToShow}");
            });
        }
    }
}

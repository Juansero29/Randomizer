using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

using Foundation;
using UIKit;

namespace Randomizer.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.SetFlags("Shell_Experimental", "Visual_Experimental", "CollectionView_Experimental", "FastRenderers_Experimental");
            AppCenter.Start("a297d040-a412-45e8-845a-7d0794e1342c", typeof(Analytics), typeof(Crashes));
            EnigmatiKreations.Framework.Controls.Platforms.iOS.Tools.Init();
            Xamarin.Forms.Svg.iOS.SvgImage.Init();  
            global::Xamarin.Forms.Forms.Init();
            #region Code for starting up the Xamarin Test Cloud Agent

            // Newer version of Visual Studio for Mac and Visual Studio provide the
            // ENABLE_TEST_CLOUD compiler directive to prevent the Calabash DLL from
            // being included in the released version of the application.
            
            Xamarin.Calabash.Start();
            
            #endregion
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}

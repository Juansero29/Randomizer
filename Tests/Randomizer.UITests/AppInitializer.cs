using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using Xamarin.UITest.Utils;

namespace UITests
{

    public class WaitTimes : IWaitTimes
    {
        public TimeSpan GestureWaitTimeout
        {
            get { return TimeSpan.FromMinutes(1); }
        }
        public TimeSpan WaitForTimeout
        {
            get { return TimeSpan.FromMinutes(1); }
        }

        public TimeSpan GestureCompletionTimeout
        {
            get => TimeSpan.FromMinutes(1);
        }
    }

    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                //..\..\Randomizer\Randomizer.Android\bin\Release\fr.ClubInfo.Randomizer-Signed.apk"
                return ConfigureApp.Android
                    .ApkFile("./com.EnigmatiKreations.Randomizer.apk")
                    .EnableLocalScreenshots()
                    .WaitTimes(new WaitTimes())
                    .StartApp();
            }
            //..\..\Randomizer\Randomizer.iOS\ipa\Randomizer.iOS.ipa
            return ConfigureApp.iOS
                .AppBundle("../../../../App/Targets/Randomizer.iOS/bin/iPhoneSimulator/Release/Randomizer.iOS.app")
                .DeviceIdentifier("2C5B5E3B-589F-40C5-92D7-9D6E54E428CF")
                .EnableLocalScreenshots()
                .WaitTimes(new WaitTimes())
                .StartApp();
        }
    }
}


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
                    .ApkFile("../../../../Randomizer/Randomizer.Android/bin/Release/fr.ClubInfo.Randomizer-Signed.apk")
                    .EnableLocalScreenshots()
                    .WaitTimes(new WaitTimes())
                    .StartApp();
            }

            //..\..\Randomizer\Randomizer.iOS\ipa\Randomizer.iOS.ipa
            return ConfigureApp.iOS
                .AppBundle("../../../../Randomizer/Randomizer.iOS/bin/iPhoneSimulator/Debug/device-builds/iphone11.4-12.2/Randomizer.iOS.app")
                .DeviceIdentifier("FF659381-1BF8-469D-8BAF-2038D9715B4F")
                .PreferIdeSettings()
                .EnableLocalScreenshots()
                .WaitTimes(new WaitTimes())
                .Debug()
                .StartApp();
        }
    }
}


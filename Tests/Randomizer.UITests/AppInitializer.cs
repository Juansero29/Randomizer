using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITests
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                //..\..\Randomizer\Randomizer.Android\bin\Release\fr.ClubInfo.Randomizer-Signed.apk"
                return ConfigureApp.Android.ApkFile("..\\..\\..\\..\\Randomizer\\Randomizer.Android\\bin\\Release\\fr.ClubInfo.Randomizer-Signed.apk").EnableLocalScreenshots().StartApp();
            }

            //..\..\Randomizer\Randomizer.iOS\ipa\Randomizer.iOS.ipa
            return ConfigureApp.iOS.AppBundle("..\\..\\..\\..\\Randomizer\\Randomizer.iOS\\bin\\iPhone\\Release\\Randomizer.iOS.app").EnableLocalScreenshots().StartApp();
        }
    }
}
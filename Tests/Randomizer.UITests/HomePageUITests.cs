using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;


namespace UITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class HomePageUITests
    {
        IApp app;
        readonly Platform platform;


        public HomePageUITests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void CanGoFromHomePageToNewListPageTest()
        {
            // We simulate navigation to the new list page
            app.Tap("FloatingButton");
            app.Screenshot("When I click on the '+' floating action button");

            if (platform == Platform.iOS)
            {
                // We try to get an element containing the text 'New List' and get the Text property inside that element
                var title = app.Query(Randomizer.Framework.Services.Resources.TextResources.NewListPageTitle).Where(t => t.Class.Equals("UILabel")).FirstOrDefault().Text;
                // We test that the title should be 'New List' because we should be in another page
                
                title.Should().Be(Randomizer.Framework.Services.Resources.TextResources.NewListPageTitle);
            }

            if (platform == Platform.Android)
            {
                // We try to get an element containing the text 'New List' and get the Text property inside that element
                //var title = app.Query(Randomizer.Framework.Services.Resources.TextResources.NewListPageTitle).First().Text;

                // We test that the title should be 'New List' because we should be in another page
                //title.Should().Be(Randomizer.Framework.Services.Resources.TextResources.NewListPageTitle);
            }

        }
    }
}

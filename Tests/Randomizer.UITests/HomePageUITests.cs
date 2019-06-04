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
        Platform platform;

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

            // We try to get an element containing the text 'New List' and get the Text property inside that element
            var title = app.Query("New List").Single().Text;

            // We test that the title should be 'New List' because we should be in another page
            title.Should().Be("New List");
        }
    }
}

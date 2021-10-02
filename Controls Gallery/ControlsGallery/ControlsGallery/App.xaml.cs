using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Svg;
using Xamarin.Forms.Xaml;

namespace ControlsGallery
{
    public partial class App : Application
    {
        public ResourceDictionary ThemeResourceDictionary { get; private set; }

        public App()
        {
            InitializeComponent();
            ThemeResourceDictionary = GetCurrentThemesResourceDictionary();
            SvgImageSource.RegisterAssembly();
            MainPage = new MainPage();
        }

        private ResourceDictionary GetCurrentThemesResourceDictionary()
        {
            return GetFirstResourceDictionaryWithOneMergedDictionary();
        }

        private ResourceDictionary GetFirstResourceDictionaryWithOneMergedDictionary()
        {
            return Application.Current.Resources.MergedDictionaries.Where(d => d.MergedDictionaries.Count == 1).FirstOrDefault();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

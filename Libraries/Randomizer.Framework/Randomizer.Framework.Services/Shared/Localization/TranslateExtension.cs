using Randomizer.Framework.Services.i18n;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Randomizer.Framework.Services.Shared.Localization
{
    [ContentProperty("ResourceName")]
    public class TranslateExtension : IMarkupExtension
    {
        readonly CultureInfo ci = null;
        const string ResourceId = "Randomizer.Framework.Services.Resources.TextResources";

        static readonly Lazy<ResourceManager> ResMgr = new Lazy<ResourceManager>(
            () => new ResourceManager(ResourceId, IntrospectionExtensions.GetTypeInfo(typeof(TranslateExtension)).Assembly));

        public string ResourceKey { get; set; }

        public TranslateExtension()
        {
            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            {
                ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
                ci = Resources.TextResources.Culture = new CultureInfo("es-ES");
            }
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (ResourceKey == null)
                return string.Empty;

            var translation = ResMgr.Value.GetString(ResourceKey, ci);
            if (translation == null)
            {
#if DEBUG
                throw new ArgumentException(
                    string.Format("Key '{0}' was not found in resources '{1}' for culture '{2}'.", ResourceKey, ResourceId, ci.Name),
                    "Text");
#else
                translation = Text;
#endif
            }
            return translation;
        }
    }
}

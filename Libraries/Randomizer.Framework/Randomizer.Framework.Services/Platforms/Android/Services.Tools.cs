using Randomizer.Framework.Services.Platforms.Droid.i18n;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Randomizer.Framework.Services.Platforms.Droid
{
    public static class Tools
    {
        public static void Init()
        {
            System.Diagnostics.Debug.WriteLine($"Initialized {typeof(LocalizationService)}");
        }
    }
}

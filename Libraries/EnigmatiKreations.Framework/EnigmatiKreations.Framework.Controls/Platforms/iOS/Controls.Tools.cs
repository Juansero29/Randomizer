using EnigmatiKreations.Framework.Controls.Platforms.iOS.CustomRenderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmatiKreations.Framework.Controls.Platforms.iOS
{
    public static class Tools
    {
        public static void Init()
        {
            System.Diagnostics.Debug.WriteLine($"Initialized{typeof(FABMenuRenderer)}");
            System.Diagnostics.Debug.WriteLine($"Initialized{typeof(FABRenderer)}");
        }
    }
}

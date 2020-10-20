using EnigmatiKreations.Framework.Controls.Platforms.Droid.CustomRenderers;
using EnigmatiKreations.Framework.Controls.Platforms.Droid.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmatiKreations.Framework.Controls.Platforms.Droid
{
    public static class Tools
    {
        public static void Init()
        {
            System.Diagnostics.Debug.WriteLine($"Initialized {typeof(RandomizerShellRenderer)}");
            System.Diagnostics.Debug.WriteLine($"Initialized {typeof(FABMenuRenderer)}");
            System.Diagnostics.Debug.WriteLine($"Initialized {typeof(FABRenderer)}");
        }
    }
}

using Android.Content;
using Randomizer.Framework.Controls.Platforms.Droid.Shell;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(Shell), typeof(RandomizerShellRenderer))]
namespace Randomizer.Framework.Controls.Platforms.Droid.Shell
{
    public class RandomizerShellRenderer : ShellRenderer
    {
        public RandomizerShellRenderer(Context context) : base(context)
        {
        }

        protected override IShellToolbarAppearanceTracker CreateToolbarAppearanceTracker()
        {
            return new RandomizerShellToolbarAppearanceTracker(this);
        }
    }
}

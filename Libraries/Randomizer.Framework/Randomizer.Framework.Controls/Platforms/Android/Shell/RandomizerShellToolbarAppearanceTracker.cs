using Android.Support.V7.Widget;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Randomizer.Framework.Controls.Platforms.Droid.Shell
{

    // Important: 
    // It's only necessary to add the ExportRendererAttribute to a custom renderer
    // that derives from the ShellRenderer class. Additional subclassed Shell renderer
    // classes are created by the subclassed ShellRenderer class.
    // From https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/shell/customrenderers

    /// <summary>
    /// This renderer hides the <see cref="Toolbar"/> on Android
    /// </summary>
    public class RandomizerShellToolbarAppearanceTracker : ShellToolbarAppearanceTracker
    {
        public RandomizerShellToolbarAppearanceTracker(IShellContext shellContext) : base(shellContext)
        {
        }

        public override void SetAppearance(Toolbar toolbar, IShellToolbarTracker toolbarTracker, ShellAppearance appearance)
        {
            base.SetAppearance(toolbar, toolbarTracker, appearance);
            toolbar.Visibility = ViewStates.Gone;
        }
    }
}

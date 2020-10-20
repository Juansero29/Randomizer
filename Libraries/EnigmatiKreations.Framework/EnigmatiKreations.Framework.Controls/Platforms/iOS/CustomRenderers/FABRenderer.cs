using CoreGraphics;
using EnigmatiKreations.Framework.Controls;
using EnigmatiKreations.Framework.Controls.iOS;
using EnigmatiKreations.Framework.Controls.Platforms.iOS.CustomRenderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(FAB), typeof(FABRenderer))]
namespace EnigmatiKreations.Framework.Controls.Platforms.iOS.CustomRenderers
{

    /// <summary>
    /// The iOS renderer for a <see cref="FAB"/> using the native <see cref="LiquidFloatingCell"/> control
    /// </summary>
    public class FABRenderer : ViewRenderer<FAB, LiquidFloatingCell>
    {
        #region Private Fields
        private LiquidFloatingCell _FAB;
        #endregion

        #region Renderer Initialization
        protected override void OnElementChanged(ElementChangedEventArgs<FAB> e)
        {
            base.OnElementChanged(e);

            if (Control != null || Element == null) return;

            var frame = new CGRect(0, 0, 50, 50);
            var image = UIImage.FromBundle(Element.ImageName);
            var fab = new LiquidFloatingCell(image)
            {
                Color = Element.ColorNormal.ToUIColor(),
                Responsible = true,
                UserInteractionEnabled = true,
                Frame = frame
            };

            fab.Touched += Fab_Touched;

            _FAB = fab;

            SetNativeControl(fab);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Handler touch floating action button
        /// </summary>
        /// <param name="point"></param>
        /// <param name="uievent"></param>
        /// <returns></returns>
        public override UIView HitTest(CGPoint point, UIEvent uievent)
        {
            if (_FAB.Frame.Contains(point)) return _FAB;
            return base.HitTest(point, uievent);
        }

        private void Fab_Touched(object sender, EventArgs e)
        {
            Element?.RaiseClicked();
        }
        #endregion
    }
}

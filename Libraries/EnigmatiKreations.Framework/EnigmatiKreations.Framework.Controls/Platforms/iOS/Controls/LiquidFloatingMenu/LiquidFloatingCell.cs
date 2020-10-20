// ========================================================================
// Module       : NomadMobile.iOS - Source File
// Author       : Bill Holmes & Juan Rodríguez
// Creation date: 2018-05-25
// ========================================================================

using System;
using System.ComponentModel;

#if __UNIFIED__
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;
#else
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using CGRect = System.Drawing.RectangleF;
using CGSize = System.Drawing.SizeF;
using CGPoint = System.Drawing.PointF;
using nfloat = System.Single;
#endif

namespace EnigmatiKreations.Framework.Controls.iOS
{
    public class LiquidFloatingCell : LiquittableCircle
    {
        #region Private Fields
        private nfloat imageRatio = 0.5f;
        private WeakReference<LiquidFloatingActionButton> actionButton = new WeakReference<LiquidFloatingActionButton>(null);
        private UIImageView imageView;
        private UIColor originalColor = UIColor.Clear;
        #endregion

        #region Properties
        public UIView View { get; private set; }

        public LiquidFloatingActionButton ActionButton
        {
            get
            {
                actionButton.TryGetTarget(out LiquidFloatingActionButton result);
                return result;
            }
            internal set { actionButton.SetTarget(value); }
        }

        public int Id { get; set; }

        public bool Responsible { get; set; }


        #endregion

        #region Events
        /// <summary>
        /// Raised when this <see cref="LiquidFloatingCell"/>  is touched
        /// </summary>
        /// <remarks>
        /// It is raised when the <see cref="TouchesBegan(NSSet, UIEvent)"/> method is called
        /// </remarks>
        public event EventHandler<EventArgs> Touched;

        public void RaiseTouched()
        {
            Touched?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region Constructor(s)
        public LiquidFloatingCell(UIImage icon)
        {
            Setup(icon);
        }

        public LiquidFloatingCell(UIImage icon, nfloat imageRatio)
        {
            this.imageRatio = imageRatio;
            Setup(icon);
        }

        public LiquidFloatingCell(UIView view)
        {
            SetupView(view);
        }
        #endregion

        #region Methods
        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            if (imageView != null)
            {
                var radius = Frame.Width * imageRatio;
                var offset = (Frame.Width - radius) / 2f;
                imageView.Frame = new CGRect(offset, offset, radius, radius);
            }
        }


        private void Setup(UIImage image, UIColor tintColor = null)
        {
            imageView = new UIImageView
            {
                Image = image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate),
                TintColor = tintColor ?? UIColor.White
            };
            SetupView(imageView);
        }

        private void SetupView(UIView view)
        {
            View = view;
            Responsible = true;
            UserInteractionEnabled = false;
            AddSubview(view);
        }

        public void Update(nfloat key, bool open)
        {
            foreach (var view in Subviews)
            {
                var ratio = (nfloat)Math.Max(2f * (key * key - 0.5f), 0f);
                view.Alpha = open ? ratio : -ratio;
            }
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
            if (Responsible)
            {
                originalColor = Color;
                Color = originalColor.White(0.5f);
                SetNeedsDisplay();
            }

            RaiseTouched();

            var button = ActionButton;
            if (button != null)
            {
                button.OnCellTapped();
            }

        }

        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);
            if (Responsible)
            {
                Color = originalColor;
                SetNeedsDisplay();
            }
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);
            Color = originalColor;

            var button = ActionButton;
            if (button != null)
            {
                button.OnCellSelected(this);
            }

        }
        #endregion
    }
}

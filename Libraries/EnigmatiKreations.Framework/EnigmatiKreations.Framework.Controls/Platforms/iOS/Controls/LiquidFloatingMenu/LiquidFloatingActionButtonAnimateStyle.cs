﻿// ========================================================================
// Module       : NomadMobile.iOS - Source File
// Author       : Bill Holmes
// Creation date: 2018-05-25
// ========================================================================

using System;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;

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
    public enum AnimateStyle
    {
        Up,
        Right,
        Left,
        Down
    }
}

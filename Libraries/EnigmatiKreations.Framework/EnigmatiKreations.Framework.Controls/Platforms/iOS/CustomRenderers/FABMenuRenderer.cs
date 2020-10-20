// ========================================================================
// Module       : NomadMobile.iOS - Source File
// Author       : Bill Holmes & Juan Rodríguez
// Creation date: 2018-05-25
// ========================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using CoreGraphics;
using EnigmatiKreations.Framework.Controls;
using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using EnigmatiKreations.Framework.Controls.iOS;
using EnigmatiKreations.Framework.Controls.Platforms.iOS.CustomRenderers;

[assembly: ExportRenderer(typeof(FABMenu), typeof(FABMenuRenderer))]
namespace EnigmatiKreations.Framework.Controls.Platforms.iOS.CustomRenderers
{
    public class FABMenuRenderer : ViewRenderer<FABMenu, LiquidFloatingActionButton>
    {

        private LiquidFloatingActionButton _menuFab;

        /// <summary>
        /// Render the control
        /// </summary>
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<FABMenu> e)
        {
            // Initialize control
            if (Control == null)
            {
                // Initialize menu
                var frame = new CGRect(0, 0, 50, 50);
                _menuFab = new LiquidFloatingActionButton(frame)
                {
                    AnimateStyle = AnimateStyle.Up,
                    EnableShadow = true,
                    Color = e.NewElement.ColorNormal.ToUIColor(),
                };

                if (Element.IsMenu)
                {
                    _menuFab.Cells = Element.Children.Select(
                     btn =>
                         new LiquidFloatingCell(UIImage.FromBundle(btn.ImageName))
                         {
                             Color = btn.ColorNormal.ToUIColor(),
                             Responsible = true,
                             UserInteractionEnabled = true,
                             Id = btn.ClickId
                         }).ToList().Reverse<LiquidFloatingCell>();
                    // I reverse the elements so that the buttons appear at the same Android order (in the exact order we put them in XAML)

                    Element.Show = delegate { _menuFab.Open(); };
                    Element.Hide = delegate { _menuFab.Close(); };
                }

                Element.GetFabIsOpen = () => !_menuFab.IsClosed;



                _menuFab.TouchDown += _menuFab_TouchDown;

                // Set native control
                SetNativeControl(_menuFab);
            }

            if (e.OldElement != null)
            {
                // Unregister
                System.Diagnostics.Debug.Write("unregister cell selected");
                _menuFab.CellSelected -= MenuFabCellSelected;
            }

            if (e.NewElement != null)
            {
                // Register
                System.Diagnostics.Debug.Write("register cell selected");
                _menuFab.CellSelected += MenuFabCellSelected;
                _menuFab.CellTapped += _menuFab_CellTapped;
            }
        }

        private void _menuFab_CellTapped(object sender, EventArgs e)
        {
            Element.RaiseMenuButtonClicked();
        }

        private void _menuFab_TouchDown(object sender, EventArgs e)
        {
            Element.RaiseMenuButtonClicked();
        }

        /// <summary>
        /// handle click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuFabCellSelected(object sender, CellSelectedEventArgs e)
        {
            // Get item click index
            var selectedCellIndex = e.Index;
            Element.RaiseSelectIndexChanged(selectedCellIndex);
        }

        /// <summary>
        /// refresh itemsource
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == FABMenu.ChildrenProperty.PropertyName)
            {
                var cells =
                    Element.Children.Select(
                        btn => new LiquidFloatingCell(UIImage.FromBundle(btn.ImageName))
                        { Responsible = true }).ToList();
                _menuFab.Cells = cells;
            }
        }

        /// <summary>
        /// handle touch floating action button
        /// </summary>
        /// <param name="point"></param>
        /// <param name="uievent"></param>
        /// <returns></returns>
        public override UIView HitTest(CGPoint point, UIEvent uievent)
        {
            if (_menuFab.Frame.Contains(point)) return _menuFab;

            foreach (var cell in _menuFab.Cells)
            {
                if (cell.Frame.Contains(point))
                    return cell;
            }

            return base.HitTest(point, uievent);
        }
    }
}
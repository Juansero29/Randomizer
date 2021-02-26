using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Clans.Fab;
using EnigmatiKreations.Framework.Controls;
using EnigmatiKreations.Framework.Controls.Platforms.Droid.CustomRenderers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(FAB), typeof(FABRenderer))]
namespace EnigmatiKreations.Framework.Controls.Platforms.Droid.CustomRenderers
{
    /// <summary>
    /// The Android renderer for a <see cref="FAB"/> using the native <see cref="FloatingActionButton"/> control
    /// </summary>
    public class FABRenderer : ViewRenderer<FAB, FloatingActionButton>, Android.Views.View.IOnClickListener
    {
        #region Fields
        private readonly Context _Context;
        #endregion

        public FABRenderer(Context context) : base(context)
        {
            _Context = context;
        }



        protected override void OnElementChanged(ElementChangedEventArgs<FAB> e)
        {
            base.OnElementChanged(e);

            var newFAB = e.NewElement;

            if (Control != null || newFAB == null) return;

            var inflater = (LayoutInflater)_Context.GetSystemService(Context.LayoutInflaterService);
            var view = inflater.Inflate(Resource.Layout.itemfab, null);
            var fab = view.FindViewById<FloatingActionButton>(Resource.Id.fabitemres);

            fab.ColorNormal = newFAB.ColorNormal.ToAndroid();
            fab.ColorPressed = newFAB.ColorPressed.ToAndroid();
            fab.ColorRipple = newFAB.ColorRipple.ToAndroid();
            fab.SetShowShadow(newFAB.HasShadow);

            // Setting the size
            fab.ButtonSize = (int)newFAB.Size;

            // Setting the image 
            if (!string.IsNullOrEmpty(newFAB.ImageName))
                SetFabImage(fab, newFAB.ImageName);
            fab.Id = newFAB.ClickId;

            // Setting the detail
            fab.LabelText = newFAB.Detail;

            // Setting the clickID
            fab.Id = newFAB.ClickId;

            // Setting if this is clickable or not
            fab.Clickable = true;

            // Setting the onClickListener
            fab.SetOnClickListener(this);


            SetNativeControl(fab);
        }


        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (Element == null) return;
            switch (e.PropertyName)
            {
                case nameof(Element.ColorNormal):
                    Control.ColorNormal = Element.ColorNormal.ToAndroid();
                    break;
                case nameof(Element.ColorPressed):
                    Control.ColorPressed = Element.ColorPressed.ToAndroid();
                    break;
                case nameof(Element.ColorRipple):
                    Control.ColorRipple = Element.ColorRipple.ToAndroid();
                    break;
            }
        }
        /// <summary>
        /// Sets the image to the FAB 
        /// </summary>
        /// <param name="fab"></param>
        /// <param name="imageName"></param>
        private void SetFabImage(FloatingActionButton fab, string imageName)
        {
            if (!string.IsNullOrWhiteSpace(imageName))
            {
                try
                {
                    Task.Run(async () =>
                    {
                        var drawableNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(imageName);
                        var resources = _Context.Resources;
                        var imageResourceName = resources.GetIdentifier(drawableNameWithoutExtension, "drawable",
                            _Context.PackageName);
                        var bitmap = await BitmapFactory.DecodeResourceAsync(_Context.Resources,
                            imageResourceName);

                        var activity = Context as Activity;
                        activity?.RunOnUiThread(() =>
                        {
                            fab.SetScaleType(ImageView.ScaleType.FitCenter);
                            fab.SetImageBitmap(bitmap);
                        });
                    });
                }
                catch (Exception ex)
                {
#if DEBUG
                    throw new FileNotFoundException("There was no Android Drawable by that name.", ex);
#else
                    System.Diagnostics.Debug.Write(ex.Message);
#endif
                }
            }

        }

        /// <summary>
        /// OnClick - listen to item click
        /// </summary>
        /// <param name="v"></param>
        public void OnClick(Android.Views.View v)
        {
            if (v is FloatingActionButton)
            {
                Element.RaiseClicked();
            }
        }
    }
}

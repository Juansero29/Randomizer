// ========================================================================
// Module       : NomadMobile - Source File
// Author       : Juan Rodríguez & Đinh Chí Trung (xamarindevelopervietnam)
// Creation date: 2018-05-25
// ========================================================================

using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.CoordinatorLayout.Widget;
using AndroidX.Core.View;
using Clans.Fab;
using EnigmatiKreations.Framework.Controls;
using EnigmatiKreations.Framework.Controls.Platforms.Droid.CustomRenderers;
using Google.Android.Material.Snackbar;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Resource = EnigmatiKreations.Framework.Controls.Resource;

[assembly: ExportRenderer(typeof(FABMenu), typeof(FABMenuRenderer))]
namespace EnigmatiKreations.Framework.Controls.Platforms.Droid.CustomRenderers
{

    public class FABMenuRenderer : ViewRenderer<FABMenu, FloatingActionMenu>, FloatingActionMenu.IOnMenuToggleListener,  Android.Views.View.IOnClickListener
    {
        private readonly Context _context;
        private FloatingActionMenu _menuFab;

        public FABMenuRenderer(Context context) : base(context)
        {
            _context = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<FABMenu> e)
        {
            base.OnElementChanged(e);

            // Initialize the control
            if (Control == null)
            {
                // Initialize the menu
                var inflater = (LayoutInflater)_context.GetSystemService(Context.LayoutInflaterService);
                var view = inflater.Inflate(Resource.Layout.fabmenu, null);
                var menuFab = view.FindViewById<FloatingActionMenu>(Resource.Id.fabmenures);

                // Setting menu colors
                menuFab.MenuButtonColorNormal = e.NewElement.ColorNormal.ToAndroid();
                menuFab.MenuButtonColorPressed = e.NewElement.ColorPressed.ToAndroid();
                menuFab.MenuButtonColorRipple = e.NewElement.ColorRipple.ToAndroid();

                // Setting menu tweaks
                menuFab.MenuButtonLabelText = Element.Detail;
                menuFab.SetOnMenuToggleListener(this);
                menuFab.SetOnMenuButtonClickListener(this);
                menuFab.SetClosedOnTouchOutside(true);

                // Setting menu icon
                if (!string.IsNullOrEmpty(e.NewElement.ImageName))
                    SetFabMenuImage(menuFab, e.NewElement.ImageName);


                _menuFab = menuFab;

                if (Element.IsMenu)
                {
                    Element.Show = (bool animate) =>
                    {

                        if (!_menuFab.IsOpened)
                        {
                            _menuFab.Toggle(animate);
                            Element.RaiseMenuToggle();
                        }

                    };

                    Element.Hide = (bool animate) =>
                    {
                        if (_menuFab.IsOpened)
                        {
                            _menuFab.Toggle(animate);
                            Element.RaiseMenuToggle();
                        }

                    };
                }


                Element.GetFabIsOpen = () => Element.IsOpened;


                if (Element.IsMenu)
                {
                    // Initialize items

                    _menuFab.RemoveAllMenuButtons();
                    var listFab = Element.Children;
                    //if (listFab.Count == 0) return;
                    foreach (var btn in listFab)
                    {
                        var inflateritem = (LayoutInflater)_context.GetSystemService(Context.LayoutInflaterService);
                        var viewitem = inflateritem.Inflate(Resource.Layout.itemfab, null);
                        var fab = viewitem.FindViewById<Clans.Fab.FloatingActionButton>(Resource.Id.fabitemres);

                        // Setting the shadow
                        fab.SetShowShadow(btn.HasShadow);

                        // Setting the background color
                        fab.SetBackgroundColor(Android.Graphics.Color.Transparent);

                        // Setting the colors 
                        fab.ColorNormal = btn.ColorNormal.ToAndroid();
                        fab.ColorPressed = btn.ColorPressed.ToAndroid();
                        fab.ColorRipple = btn.ColorRipple.ToAndroid();

                        // Setting the size
                        fab.ButtonSize = (int)btn.Size;

                        // Setting the image 
                        if (!string.IsNullOrEmpty(btn.ImageName))
                            SetFabImage(fab, btn.ImageName);
                        fab.Id = btn.ClickId;

                        // Setting the detail
                        fab.LabelText = btn.Detail;

                        // Setting the clickID
                        fab.Id = btn.ClickId;

                        // Setting if this is clickable or not
                        fab.Clickable = true;

                        // Setting the onClickListener
                        fab.SetOnClickListener(this);

                        // Adding to the children
                        _menuFab.AddMenuButton(fab);
                    }
                }
             

                // Set native control
                SetNativeControl(_menuFab);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == FABMenu.ImageNameProperty.PropertyName)
            {
                // Setting menu icon
                if (!string.IsNullOrEmpty(Element.ImageName))
                    SetFabMenuImage(_menuFab, Element.ImageName);
            }

            // Handle when the children property has changed
            if (e.PropertyName == FABMenu.ChildrenProperty.PropertyName)
            {
                _menuFab.RemoveAllMenuButtons();

                var listFab = Element.Children;
                if (listFab.Count == 0) return;
                foreach (var btn in listFab)
                {
                    var inflater = (LayoutInflater)_context.GetSystemService(Context.LayoutInflaterService);
                    var view = inflater.Inflate(Resource.Layout.itemfab, null);
                    var fab = view.FindViewById<Clans.Fab.FloatingActionButton>(Resource.Id.fabitemres);

                    // Setting the shadow
                    fab.SetShowShadow(btn.HasShadow);

                    // Setting the background color
                    fab.SetBackgroundColor(Android.Graphics.Color.Transparent);

                    // Setting the colors 
                    fab.ColorNormal = btn.ColorNormal.ToAndroid();
                    fab.ColorPressed = btn.ColorPressed.ToAndroid();
                    fab.ColorRipple = btn.ColorRipple.ToAndroid();

                    // Setting the size
                    fab.ButtonSize = (int)btn.Size;

                    // Setting the image 
                    if (!string.IsNullOrEmpty(btn.ImageName))
                        SetFabImage(fab, btn.ImageName);
                    fab.Id = btn.ClickId;

                    // Setting the detail
                    fab.LabelText = btn.Detail;

                    // Setting the clickID
                    fab.Id = btn.ClickId;

                    // Setting if this is clickable or not
                    fab.Clickable = true;

                    // Setting the onClickListener
                    fab.SetOnClickListener(this);

                    // Adding to the children
                    _menuFab.AddMenuButton(fab);
                }
            }
            else if (e.PropertyName == FABMenu.DetailProperty.PropertyName)
            {
                _menuFab.MenuButtonLabelText = Element.Detail;
            }

            switch (e.PropertyName)
            {
                case nameof(Element.ColorNormal):
                    Control.MenuButtonColorNormal = Element.ColorNormal.ToAndroid();
                    break;
                case nameof(Element.ColorPressed):
                    Control.MenuButtonColorPressed = Element.ColorPressed.ToAndroid();
                    break;
                case nameof(Element.ColorRipple):
                    Control.MenuButtonColorRipple = Element.ColorRipple.ToAndroid();
                    break;
            }
        }

        /// <summary>
        /// OnMenuToggle - listen to togggle click
        /// </summary>
        /// <param name="opened"></param>
        public void OnMenuToggle(bool opened)
        {
            if (Element.IsMenu)
            {
                Element.RaiseMenuToggle();
                Element.IsOpened = opened;

                if (opened)
                {
                    // The menu has just been opened
                    Element.RaiseMenuOpened();
                }
                else
                {
                    // The menu has just been closed
                    Element.RaiseMenuClosed();
                }

                // Put here native code to do when the FAB Menu is opened
            }

        }

        /// <summary>
        /// OnClick - listen to item click
        /// </summary>
        /// <param name="v"></param>
        public void OnClick(Android.Views.View v)
        {
            if (v.Parent is FloatingActionMenu)
            {
                Element.RaiseMenuButtonClicked();
            }


            if (Element.IsMenu)
            {

                _menuFab.Toggle(animate: true);


                if (v is Clans.Fab.FloatingActionButton fab)
                {
                    Element.RaiseSelectIndexChanged(fab.Id);
                }
            }

        }

        private void SetFabMenuImage(Clans.Fab.FloatingActionMenu fabMenu, string imageName)
        {
            if (!string.IsNullOrWhiteSpace(imageName))
            {
                try
                {
                    Task.Run(() =>
                    {
                        var drawableNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(imageName);
                        var resources = _context.Resources;
                        var imageResourceName = resources.GetIdentifier(drawableNameWithoutExtension, "drawable",
                            _context.PackageName);

                        var drawable = Context.GetDrawable(imageResourceName);

                        var activity = Context as Activity;
                        activity?.RunOnUiThread(() =>
                        {
                            fabMenu.SetBackground(drawable);
                        });
                    });
                }
                catch (Exception e)
                {
#if DEBUG
                    throw new FileNotFoundException("There was no Android Drawable by that name.", e);
#else
                    System.Diagnostics.Debug.Write(e.Message);
#endif
                }
            }
        }

        /// <summary>
        /// Sets the image to the FAB 
        /// </summary>
        /// <param name="fab"></param>
        /// <param name="imageName"></param>
        private void SetFabImage(Clans.Fab.FloatingActionButton fab, string imageName)
        {
            if (!string.IsNullOrWhiteSpace(imageName))
            {
                try
                {
                    Task.Run(async () =>
                    {
                        var drawableNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(imageName);
                        var resources = _context.Resources;
                        var imageResourceName = resources.GetIdentifier(drawableNameWithoutExtension, "drawable",
                            _context.PackageName);
                        var bitmap = await BitmapFactory.DecodeResourceAsync(_context.Resources,
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

        public static int CalculateInSampleSize(BitmapFactory.Options options, int reqWidth, int reqHeight)
        {
            // Raw height and width of image
            float height = options.OutHeight;
            float width = options.OutWidth;
            double inSampleSize = 1D;

            if (height > reqHeight || width > reqWidth)
            {
                int halfHeight = (int)(height / 2);
                int halfWidth = (int)(width / 2);

                // Calculate a inSampleSize that is a power of 2 - the decoder will use a value that is a power of two anyway.
                while ((halfHeight / inSampleSize) > reqHeight && (halfWidth / inSampleSize) > reqWidth)
                {
                    inSampleSize *= 2;
                }
            }

            return (int)inSampleSize;
        }
    }

    // Behaviour
    [Register("NomadMobile.Droid.CustomRenderers.FABMenu.FloatingActionMenuBehavior")]
    public class FloatingActionMenuBehavior : CoordinatorLayout.Behavior
    {
        private float _mTranslationY;

        public FloatingActionMenuBehavior(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public override bool LayoutDependsOn(CoordinatorLayout parent, Java.Lang.Object child, Android.Views.View dependency)
        {
            return IsInstanceOf<Snackbar.SnackbarLayout>(dependency);
        }

        public override bool OnDependentViewChanged(CoordinatorLayout parent, Java.Lang.Object child, Android.Views.View dependency)
        {
            if (IsInstanceOf<FloatingActionMenu>(child) && IsInstanceOf<Snackbar.SnackbarLayout>(dependency))
            {
                UpdateTranslation(parent, (Android.Views.View)child, dependency);
            }

            return false;
        }

        public override void OnDependentViewRemoved(CoordinatorLayout parent, Java.Lang.Object child, Android.Views.View dependency)
        {
            if (IsInstanceOf<FloatingActionMenu>(child) && IsInstanceOf<Snackbar.SnackbarLayout>(dependency))
            {
                UpdateTranslation(parent, (Android.Views.View)child, dependency);
            }
        }

        private void UpdateTranslation(CoordinatorLayout parent, Android.Views.View child, Android.Views.View dependency)
        {
            var translationY = GetTranslationY(parent, child);
            if (Math.Abs(translationY - _mTranslationY) > double.Epsilon)
            {
                ViewCompat.Animate(child).Cancel();

                if (Math.Abs(Math.Abs(translationY - _mTranslationY) - dependency.Height) < double.Epsilon)
                {
                    ViewCompat.Animate(child)
                        .TranslationY(translationY)
                        .SetListener(null);
                }
                else
                {
#pragma warning disable CS0618 // Type or member is obsolete
                    ViewCompat.SetTranslationY(child, translationY);
#pragma warning restore CS0618 // Type or member is obsolete
                }

                _mTranslationY = translationY;
            }
        }

        private static float GetTranslationY(CoordinatorLayout parent, Android.Views.View child)
        {
            var minOffset = 0.0F;
            var dependencies = parent.GetDependencies(child);
            var i = 0;

            for (var z = dependencies.Count; i < z; ++i)
            {
                var view = dependencies[i];
                if (IsInstanceOf<Snackbar.SnackbarLayout>(view) && parent.DoViewsOverlap(child, view))
                {
#pragma warning disable CS0618 // Type or member is obsolete
                    minOffset = Math.Min(minOffset, ViewCompat.GetTranslationY(view) - view.Height);
#pragma warning restore CS0618 // Type or member is obsolete
                }
            }

            return minOffset;
        }

        private static bool IsInstanceOf<T>(object instance)
        {
            return instance.GetType().IsAssignableFrom(typeof(T));
        }
    }
}
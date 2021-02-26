using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace EnigmatiKreations.Framework.Controls.Floating
{


    /// <summary>
    /// Class representing a Floating Action Button
    /// </summary>
    public class FloatingActionButton : View
    {
        #region Bindable Properties

        /// <summary>
        /// Bindable property pointing to the detail text property for this FAB 
        /// </summary>
        /// <seealso cref="Detail"/>
        public static readonly BindableProperty DetailProperty = BindableProperty.Create(nameof(Detail), typeof(string), typeof(FAB), string.Empty);

        /// <summary>
        /// Bindable property pointing to the click id property for this FAB
        /// </summary>
        /// <seealso cref="ClickId"/>
        public static readonly BindableProperty ClickIdProperty = BindableProperty.Create(nameof(ClickId), typeof(int), typeof(FAB), -1);

        /// <summary>
        /// Bindable property pointing to the image name property for this FAB
        /// </summary>
        /// <seealso cref="ImageName"/>
        public static readonly BindableProperty ImageNameProperty = BindableProperty.Create(nameof(ImageName), typeof(string), typeof(FAB), string.Empty);

        /// <summary>
        /// Bindable property pointing to the normal color property for this FAB
        /// </summary>
        /// <seealso cref="ColorNormal"/>
        public static readonly BindableProperty ColorNormalProperty = BindableProperty.Create(nameof(ColorNormal), typeof(Color), typeof(FAB), Color.Accent);

        /// <summary>
        /// Bindable property pointing to the pressed color property for this FAB
        /// </summary>
        /// <seealso cref="ColorPressed"/>
        public static readonly BindableProperty ColorPressedProperty = BindableProperty.Create(nameof(ColorPressed), typeof(Color), typeof(FAB), Color.Accent);

        /// <summary>
        /// Bindable property pointing to the ripple color property for this FAB
        /// </summary>
        /// <seealso cref="ColorRipple"/>
        public static readonly BindableProperty ColorRippleProperty = BindableProperty.Create(nameof(ColorRipple), typeof(Color), typeof(FAB), Color.Accent);

        /// <summary>
        /// Bindable property pointing to the size property for this FAB
        /// </summary>
        /// <seealso cref="Size"/>
        public static readonly BindableProperty SizeProperty = BindableProperty.Create(nameof(Size), typeof(FloatingActionButtonSize), typeof(FAB), FloatingActionButtonSize.Normal);

        /// <summary>
        /// Bindable property pointing to the has shadow property for this FAB
        /// </summary>
        /// <seealso cref="HasShadow"/>
        public static readonly BindableProperty HasShadowProperty = BindableProperty.Create(nameof(HasShadow), typeof(bool), typeof(FAB), true);



        #region ClickedCommand
        public static readonly BindableProperty ClickedCommandProperty = BindableProperty.Create(nameof(ClickedCommand), typeof(ICommand), typeof(FAB), propertyChanged: (obj, old, newV) =>
        {
            var me = obj as FAB;
            if (!(newV is ICommand)) return;
            var oldClicked = (ICommand)old;
            var newClicked = (ICommand)newV;
            me?.ClickedChanged(oldClicked, newClicked);
        });

        private void ClickedChanged(ICommand _, ICommand _1)
        {

        }

        /// <summary>
        /// Command to execute when the FAB is clicked (or tapped)
        /// </summary>
        public ICommand ClickedCommand
        {
            get => (ICommand)GetValue(ClickedCommandProperty);
            set => SetValue(ClickedCommandProperty, value);
        }
        #endregion




        #endregion

        #region Properties

        /// <summary>
        /// The text that describes the main action of this FAB
        /// </summary>
        public string Detail
        {
            get { return (string)GetValue(DetailProperty); }
            set { SetValue(DetailProperty, value); }
        }

        /// <summary>
        /// The click id for this FAB
        /// </summary>
        /// <remarks>
        /// This allows to identify which menu button has been pressed when responding to the SelectIndexChanged event of a FABMenu
        /// </remarks>
        public int ClickId
        {
            get { return (int)GetValue(ClickIdProperty); }
            set { SetValue(ClickIdProperty, value); }
        }

        /// <summary>
        /// The name of the image this FAB should print to the screen
        /// </summary>
        public string ImageName
        {
            get { return (string)GetValue(ImageNameProperty); }
            set { SetValue(ImageNameProperty, value); }
        }

        /// <summary>
        /// The normal color for this FAB 
        /// </summary>
        /// <remarks>
        /// The color is normal when the button isn't pressed and isn't rippling
        /// </remarks>
        public Color ColorNormal
        {
            get { return (Color)GetValue(ColorNormalProperty); }
            set { SetValue(ColorNormalProperty, value); }
        }

        /// <summary>
        /// The color of this FAB when it is pressed (while the finger is on it)
        /// </summary>
        public Color ColorPressed
        {
            get { return (Color)GetValue(ColorPressedProperty); }
            set { SetValue(ColorPressedProperty, value); }
        }

        /// <summary>
        /// The color of this FAB when it is rippling (being animated after having pressed it)
        /// </summary>
        public Color ColorRipple
        {
            get { return (Color)GetValue(ColorRippleProperty); }
            set { SetValue(ColorRippleProperty, value); }
        }

        /// <summary>
        /// The size property for this FAB
        /// </summary>
        public FloatingActionButtonSize Size
        {
            get { return (FloatingActionButtonSize)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        /// <summary>
        /// Defines whether this FAB has a shadow or not
        /// </summary>
        public bool HasShadow
        {
            get { return (bool)GetValue(HasShadowProperty); }
            set { SetValue(HasShadowProperty, value); }
        }

        #endregion

        #region Events
        public event EventHandler<EventArgs> Clicked;

        public void RaiseClicked()
        {
            Clicked?.Invoke(this, new FABMenuIndexChangedArgs(nameof(Clicked), this));
            if (ClickedCommand == null) return;

            if (ClickedCommand.CanExecute(this))
            {
                ClickedCommand.Execute(this);
            }
        }

        #endregion

    }

}

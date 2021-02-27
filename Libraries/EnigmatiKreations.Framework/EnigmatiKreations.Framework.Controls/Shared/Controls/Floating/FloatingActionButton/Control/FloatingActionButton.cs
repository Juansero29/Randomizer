using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;

namespace EnigmatiKreations.Framework.Controls.Floating
{


    /// <summary>
    /// Class representing a Floating Action Button
    /// </summary>

    public class FloatingActionButton : ContentView
    {

        #region Template Parts
    
        #region Button
        private const string PART_Button = "PART_Button";
        private Button ButtonPart;
        #endregion
        private const string PART_Path = "PART_Path";
        private Path PathPart;

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PathPart = GetTemplateChild(PART_Path) as Path;
            ButtonPart = GetTemplateChild(PART_Button) as Button;

            ButtonPart.Clicked += ButtonPart_Clicked;
        }

        private void ButtonPart_Clicked(object sender, EventArgs e)
        {
            RaiseClicked();
        }
        #endregion

        #region Bindable Properties
        #region Detail
        public static readonly BindableProperty DetailProperty = BindableProperty.Create(nameof(Detail), typeof(string), typeof(FloatingActionButton), propertyChanged: (obj, old, newV) =>
        {
            var me = obj as FloatingActionButton;
            if (newV != null && !(newV is string)) return;
            var oldDetail = (string)old;
            var newDetail = (string)newV;
            me?.DetailChanged(oldDetail, newDetail);
        });

        private void DetailChanged(string oldDetail, string newDetail)
        {

        }

        /// <summary>
        /// The detail label for this <see cref="FloatingActionButton"/>
        /// </summary>
        public string Detail
        {
            get => (string)GetValue(DetailProperty);
            set => SetValue(DetailProperty, value);
        }
        #endregion

        #region ColorNormal
        public static readonly BindableProperty ColorNormalProperty = BindableProperty.Create(nameof(ColorNormal), typeof(Color), typeof(FloatingActionButton), propertyChanged: (obj, old, newV) =>
        {
            var me = obj as FloatingActionButton;
            if (newV != null && !(newV is Color)) return;
            var oldColorNormal = (Color)old;
            var newColorNormal = (Color)newV;
            me?.ColorNormalChanged(oldColorNormal, newColorNormal);
        });

        private void ColorNormalChanged(Color oldColorNormal, Color newColorNormal)
        {

        }

        /// <summary>
        /// The color when this <see cref="FloatingActionButton"/> is in a normal state
        /// </summary>
        public Color ColorNormal
        {
            get => (Color)GetValue(ColorNormalProperty);
            set => SetValue(ColorNormalProperty, value);
        }
        #endregion

        #region ImageName
        public static readonly BindableProperty ImageNameProperty = BindableProperty.Create(nameof(ImageName), typeof(string), typeof(FloatingActionButton), propertyChanged: (obj, old, newV) =>
        {
            var me = obj as FloatingActionButton;
            if (newV != null && !(newV is string)) return;
            var oldImageName = (string)old;
            var newImageName = (string)newV;
            me?.ImageNameChanged(oldImageName, newImageName);
        });

        private void ImageNameChanged(string oldImageName, string newImageName)
        {

        }

        /// <summary>
        /// The image name to use and put inside of this <see cref="FloatingActionButton"/>
        /// </summary>
        public string ImageName
        {
            get => (string)GetValue(ImageNameProperty);
            set => SetValue(ImageNameProperty, value);
        }
        #endregion

        #region ColorPressed
        public static readonly BindableProperty ColorPressedProperty = BindableProperty.Create(nameof(ColorPressed), typeof(Color), typeof(FloatingActionButton), propertyChanged: (obj, old, newV) =>
        {
            var me = obj as FloatingActionButton;
            if (newV != null && !(newV is Color)) return;
            var oldColorPressed = (Color)old;
            var newColorPressed = (Color)newV;
            me?.ColorPressedChanged(oldColorPressed, newColorPressed);
        });

        private void ColorPressedChanged(Color oldColorPressed, Color newColorPressed)
        {

        }

        /// <summary>
        /// The color to use when this <see cref="FloatingActionButton"/> is pressed by the user
        /// </summary>
        public Color ColorPressed
        {
            get => (Color)GetValue(ColorPressedProperty);
            set => SetValue(ColorPressedProperty, value);
        }
        #endregion

        #region ColorRipple
        public static readonly BindableProperty ColorRippleProperty = BindableProperty.Create(nameof(ColorRipple), typeof(Color), typeof(FloatingActionButton), propertyChanged: (obj, old, newV) =>
        {
            var me = obj as FloatingActionButton;
            if (newV != null && !(newV is Color)) return;
            var oldColorRipple = (Color)old;
            var newColorRipple = (Color)newV;
            me?.ColorRippleChanged(oldColorRipple, newColorRipple);
        });

        private void ColorRippleChanged(Color oldColorRipple, Color newColorRipple)
        {

        }

        /// <summary>
        /// The color to use for this <see cref="FloatinActionMenu"/> for the ripple created after the user pressed 
        /// </summary>
        public Color ColorRipple
        {
            get => (Color)GetValue(ColorRippleProperty);
            set => SetValue(ColorRippleProperty, value);
        }
        #endregion

        #region ClickedCommand
        public static readonly BindableProperty ClickedCommandProperty = BindableProperty.Create(nameof(ClickedCommand), typeof(ICommand), typeof(FloatingActionButton), propertyChanged: (obj, old, newV) =>
        {
            var me = obj as FloatingActionButton;
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

        #region Size
        public static readonly BindableProperty SizeProperty = BindableProperty.Create(nameof(Size), typeof(FloatingActionButtonSize), typeof(FloatingActionButton), propertyChanged: (obj, old, newV) =>
        {
            var me = obj as FloatingActionButton;
            if (newV != null && !(newV is FloatingActionButtonSize)) return;
            var oldSize = (FloatingActionButtonSize)old;
            var newSize = (FloatingActionButtonSize)newV;
            me?.SizeChanged(oldSize, newSize);
        });

        private void SizeChanged(FloatingActionButtonSize oldSize, FloatingActionButtonSize newSize)
        {

        }

        /// <summary>
        /// The size for this <see cref="FloatingActionButton"/>
        /// </summary>
        public FloatingActionButtonSize Size
        {
            get => (FloatingActionButtonSize)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
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

            PathPart.Layout(PathPart.Bounds);
        }

        #endregion



    }

}
